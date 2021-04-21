using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IAttackable {
    public float health = 10;
    public float maxHealth = 10;

    /**
     * Used to destory an other GameObject (e.g. the parent GameObject) 
     */
    public GameObject completeGameObject;

    public GameObject healthBarUI;
    public Slider slider;

    private void Awake() {
        if (!completeGameObject) {
            completeGameObject = gameObject;
        }
    }

    // Start is called before the first frame update
    void Start() {
        health = maxHealth;
        if (slider) {
            slider.value = GetPercentageHealth();
        }
    }

    // Update is called once per frame
    public void Update() {
        if (slider) {
            slider.value = GetPercentageHealth();
        }

        if (health < maxHealth) {
            healthBarUI.SetActive(true);
        }

        if (health <= 0) {
            Destroy(gameObject);
        }

        if (health > maxHealth) {
            health = maxHealth;
        }

        if (health >= maxHealth && healthBarUI) {
            healthBarUI.SetActive(false);
        }
    }

    public float GetPercentageHealth() {
        return health / maxHealth;
    }

    public void Attack(float damage) {
        this.health -= damage;
        if (this.health <= 0) {
            Destroy(completeGameObject);
        }
    }

    //Orient the camera after all movement is completed this frame to avoid jittering
    void LateUpdate() {
        if (slider) {
            slider.transform.LookAt(slider.transform.position + GameSettings.Instance.cam.transform.rotation * Vector3.forward,
                GameSettings.Instance.cam.transform.rotation * Vector3.up);
        }
    }
}