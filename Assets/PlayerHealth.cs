using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health, IAttackable {
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        base.Update();
    }

    public void Attack(float damage) {
        health -= damage;
        if (health <= 0) {
            Debug.Log("Player Died");
            Debug.Log("Respawn ... ");
            health = maxHealth;
        }
        GameSettings.Instance.playerInfoHandler.UpdateHealth(this.GetPercentageHealth());

    }
    
}