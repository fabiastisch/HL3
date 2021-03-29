using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpAndDrop : MonoBehaviour {
  
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            //PickUp();
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            //Drop();
        }
    }

}