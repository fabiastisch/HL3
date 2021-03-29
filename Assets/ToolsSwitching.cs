using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ToolsSwitching : MonoBehaviour {
    public int selectedTool = 0;
    public int prevSelectedTool = 0;
    public GameObject buildingTool;
    private bool isBuilding = false;

    // Start is called before the first frame update
    void Start() {
        UpdateSelectedTool();
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

    // Update is called once per frame
    void Update() {
        int prevSelected = selectedTool;
        if (Input.GetKeyDown(KeyCode.Q)) {
            isBuilding = !isBuilding;
            if (isBuilding) {
                // Set Building obj active, everything else inactive.
                buildingTool.SetActive(true);
                UpdateSelectedTool();
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
            UpdateSelectedTool();
        }
    }
}