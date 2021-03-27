using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameworkSingleton : MonoBehaviour {
    
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
}