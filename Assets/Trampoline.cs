using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour {
    public float maxForce = 1000f;
    
    // Start is called before the first frame updategi
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void OnTriggerEnter(Collider other) {
        PlayerMovement playerMovement = GameSettings.Instance.playerMovement;
        playerMovement.velocity *= -1f;
        // GameSettings.Instance.playerMovement.jumpHeight *= 1.3f;
        // GameSettings.Instance.playerMovement.Jump();
        
        Debug.Log("OnTriggerEnter");
    }

    private void OnTriggerExit(Collider other) {
       //  GameSettings.Instance.playerMovement.jumpHeight /= 1/1.3f;
        Debug.Log("OnTriggerExit");
    }

    private void OnTriggerStay(Collider other) {
        Debug.Log("OnTriggerStay");
    }
}