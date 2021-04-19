using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class MyNetworkPlayer : MonoBehaviourPun {
    public Camera camera;
    
    // Start is called before the first frame update
    void Start() {
        if (photonView.IsMine) {
            
        }
        else {
            camera.enabled = false;
            camera.gameObject.GetComponent<AudioListener>().enabled = false;
            gameObject.GetComponent<FirstPersonController>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update() {
    }
}