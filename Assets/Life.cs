using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {
    public int life = 10;
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    public void Attack(int damage) {
        this.life -= damage;
        if (this.life <= 0) {
            Destroy(gameObject);
        }
    }
}