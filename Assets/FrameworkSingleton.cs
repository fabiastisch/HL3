using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameworkSingleton : MonoBehaviour {
    public bool isColliding = false;
    
    private static FrameworkSingleton instance;
    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(instance.gameObject);
            instance = this;
        }
    }

    /*private void OnTriggerEnter(Collider other) {
        Debug.Log("TEnter");
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("TExit");
    }

    private void OnTriggerStay(Collider other) {
        Debug.Log("TriggerStay");
    }*/
}