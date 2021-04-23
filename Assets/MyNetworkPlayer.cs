using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class MyNetworkPlayer : MonoBehaviourPun {
    public Camera cam;
    public GameObject visiblePlayer;
    public GameObject canvas;
    
    // Start is called before the first frame update
    void Start() {
        if (photonView) {
            if (photonView.IsMine) {
                int dontRenderLayer = LayerMask.NameToLayer("DontRender");
                int childs = visiblePlayer.transform.childCount;
                for (int i = 0; i < childs; i++) {
                    visiblePlayer.transform.GetChild(i).gameObject.layer = dontRenderLayer;
                }
                canvas.layer = dontRenderLayer;

                GameSettings settings = GameSettings.Instance;
                settings.playerMovement = GetComponent<PlayerMovement>();
                settings.cam = cam;
            }
            else {
                cam.enabled = false;
                cam.gameObject.GetComponent<AudioListener>().enabled = false;
                gameObject.GetComponent<PlayerMovement>().enabled = false;
                GetComponent<PlayerUtils>().toolsSwitchingScript.enabled = false;
                
            }
        }
        
    }

    // Update is called once per frame
    void Update() {
    }
}