using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BulletScript : MonoBehaviourPun {

    public float expireTime = 5f;

    public float damage = 20f;

    private bool hitted = false;

    public GameObject Owner { get; set; }

    // Start is called before the first frame update
    void Start() {
        if (photonView.IsMine) {
            Invoke(nameof(Die), expireTime);
        }
    }

    void Die() {
        if (gameObject) {
            PhotonNetwork.Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update() {
    }

    private void OnCollisionEnter(Collision other) {
       

        if (hitted) {
            return;
        }
        
        hitted = true;
        
        if (!photonView.IsMine) {
            GameObject hittedObj = other.gameObject;
            if (hittedObj.Equals(Owner)) {
                Debug.Log("Equals Owner...");
                return;
            }
            IAttackable healthScript = hittedObj.GetComponent(typeof(IAttackable)) as IAttackable;
            if (healthScript != null) {
                Debug.Log("HealthScript: "+healthScript);
                healthScript.Attack(damage);   
            }
        }
        else {
            PhotonNetwork.Destroy(gameObject);

        }

    }
}