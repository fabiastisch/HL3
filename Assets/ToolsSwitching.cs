using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ToolsSwitching : MonoBehaviourPunCallbacks {
    public int selectedTool = 0;
    public int prevSelectedTool = 0;
    public GameObject buildingTool;
    private bool isBuilding = false;


    // Start is called before the first frame update
    void Start() {
        UpdateFromMyView();
    }

    private void UpdateSelectedTool() {
        int i = 0;
        foreach (Transform tools in transform) {
            if (i == selectedTool && !isBuilding) {
                tools.gameObject.SetActive(true);
            }
            else {
                tools.gameObject.SetActive(false);
            }

            i++;
        }
    }

    private void UpdateFromMyView() {
        Hashtable hashtable = new Hashtable();
        hashtable.Add("itemIndex", this.selectedTool);
        hashtable.Add("isBuilding", this.isBuilding);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
        GameSettings.Instance.handMenuHandler.SetIsBuilding(isBuilding);
        GameSettings.Instance.handMenuHandler.SetCurrentSelectetItem(selectedTool);
        UpdateSelectedTool();
    }

    // Update is called once per frame
    void Update() {
        if (!photonView.IsMine) {
            return;
        }
        int prevSelected = selectedTool;
        if (Input.GetKeyDown(KeyCode.Q)) {
            isBuilding = !isBuilding;
            if (isBuilding) {
                // Set Building obj active, everything else inactive.
                buildingTool.SetActive(true);
                UpdateFromMyView();
            }
            else {
                buildingTool.SetActive(false);
                prevSelected = -1; // force auto update
            }
        }

        if (isBuilding) {
            return;
        }


        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelInput > 0f) {
            if (selectedTool >= transform.childCount - 1) {
                selectedTool = 0;
            }
            else
                selectedTool++;
        }
        else if (scrollWheelInput < 0f) {
            if (selectedTool <= 0) {
                selectedTool = transform.childCount - 1;
            }
            else
                selectedTool--;
        }


        if (prevSelected != selectedTool) {
            UpdateFromMyView();
        }
    }

    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, Hashtable changedProps) {
        if (!photonView.IsMine && targetPlayer.Equals(photonView.Owner)) {
            int itemIndex = (int) changedProps["itemIndex"];
            Debug.Log("On PlayerProp Update: " + itemIndex);
            selectedTool = itemIndex;
            isBuilding = (bool) changedProps["isBuilding"];
            buildingTool.SetActive(isBuilding);
            UpdateSelectedTool();

            
        }
    }
}