using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BombThrower : Tool {
    public GameObject bombPrefab;
    public float forwardThrowForce = 1000f;
    public float upThrowForce = 50f;
    
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (!photonView.IsMine) {
            return;
        }
        if (Input.GetButtonDown("Fire1")) {
            GameObject ball;
            if (PhotonNetwork.IsConnected) {
                ball = PhotonNetwork.Instantiate(bombPrefab.name, gameObject.transform.position, Quaternion.identity);
            }
            else {
                ball = Instantiate(bombPrefab, gameObject.transform.position, Quaternion.identity);

            }

            Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
            PlayerMovement playerMovement =  GameSettings.Instance.playerMovement;
            // Debug.Log(playerMovement.GetCurrentPlayerVelocity());
            rigidbody.velocity = playerMovement.GetCurrentPlayerVelocity();
            rigidbody.AddForce(GameSettings.Instance.cam.transform.forward * forwardThrowForce );
        }
    }
}