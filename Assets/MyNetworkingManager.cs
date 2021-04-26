using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyNetworkingManager : MonoBehaviour {
    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start() {
        if (PhotonNetwork.InRoom) {
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(-42, 5, -25), Quaternion.identity, 0);
        }
        else {
             //        PhotonNetwork.LoadLevel("World");

            // ceneManager.LoadScene("NetworkingPreGame");
        }

       //  Debug.Log("PhotonNetwork in Room?: "+PhotonNetwork.InRoom);
    }

    // Update is called once per frame
    void Update() {
    }
} 