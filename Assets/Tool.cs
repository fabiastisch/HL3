using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public abstract class Tool : MonoBehaviourPun {
    
    void Start() {
        /*gameObject.AddComponent<PhotonView>();
        gameObject.AddComponent<PhotonTransformView>();*/
        
        if (photonView) {
            if (photonView.IsMine) {
                
            }
            else {
                
            }
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

}