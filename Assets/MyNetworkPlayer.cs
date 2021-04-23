using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class MyNetworkPlayer : MonoBehaviourPun {
    public Camera cam;
    
    // Start is called before the first frame update
    void Start() {
        if (photonView.IsMine) {
            
        }
        else {
            cam.enabled = false;
            cam.gameObject.GetComponent<AudioListener>().enabled = false;
            gameObject.GetComponent<PlayerMovement>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update() {
    }
}