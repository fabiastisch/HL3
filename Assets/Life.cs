using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Life : MonoBehaviour {
    public int life = 10;
    
    /**
     * Used to destory an other GameObject (e.g. the parent GameObject) 
     */
    public GameObject completeGameObject;

    private void Awake() {
        if (!completeGameObject) {
            completeGameObject = gameObject;
        }
    }

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    public void Attack(int damage) {
        this.life -= damage;
        if (this.life <= 0) {
            Destroy(completeGameObject);
        }
    }
}