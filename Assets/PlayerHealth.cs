using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerHealth : Health, IAttackable {
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    new void Update() {
        base.Update();
    }

    public new void Attack(float damage) {
        if (photonView.IsMine) {
            health -= damage;
         //   Debug.Log("Player was Attacked for " + damage + ", current Life: " + health);
            if (health <= 0) {
                Debug.Log("Player Died");
                Debug.Log("Respawn ... ");
                health = maxHealth;
                GameSettings.Instance.playerMovement.Die();
            }

            GameSettings.Instance.playerInfoHandler.UpdateHealth(this.GetPercentageHealth());
        }
        else {
            photonView.RPC(nameof(RPCCall), RpcTarget.All, damage );
        }
    }

    private void FindPlayer() {
        
        Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;
        
        foreach (var player in players) {
            
        }
    }

    [PunRPC]
    void RPCCall(float damage) {
        if (!photonView.IsMine) {
            return;
        }
        health -= damage;
       //  Debug.Log("Player was Attacked for " + damage + ", current Life: " + health);
        if (health <= 0) {
            Debug.Log("Player Died");
            Debug.Log("Respawn ... ");
            health = maxHealth;
            GameSettings.Instance.playerMovement.Die();
        }

        GameSettings.Instance.playerInfoHandler.UpdateHealth(this.GetPercentageHealth());
    }
    
}