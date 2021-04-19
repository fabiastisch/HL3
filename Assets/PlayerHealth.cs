using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health {
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Attack(float damage) {
        Debug.Log("Player was attacked");
        base.Attack(damage);
        GameSettings.Instance.playerInfoHandler.UpdateHealth(this.health);
    }
}