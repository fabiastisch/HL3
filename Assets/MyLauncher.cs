using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class MyLauncher : MonoBehaviourPunCallbacks {

    public void Connect() {
        if (PhotonNetwork.IsConnected) {
            PhotonNetwork.JoinRandomRoom();
        }
        else {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinRandomRoom();
        // PhotonNetwork.JoinLobby();
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message) {
        PhotonNetwork.CreateRoom("Tester Room", new RoomOptions {MaxPlayers = 4});
    }

    public override void OnJoinedRoom() {
        Debug.Log("Room Joined!");
        PhotonNetwork.LoadLevel("World");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList) {
        Debug.Log("Room Update. Number of rooms: " + roomList.Count );
        foreach (RoomInfo ri in roomList) {
            Debug.Log(ri.Name);
        }
    }

    // Start is called before the first frame update
    void Start() {
    }

}