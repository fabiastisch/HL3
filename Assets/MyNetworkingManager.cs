using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MyNetworkingManager : MonoBehaviour {
    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start() {
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 5, 0), Quaternion.identity, 0);
    }

    // Update is called once per frame
    void Update() {
    }
}