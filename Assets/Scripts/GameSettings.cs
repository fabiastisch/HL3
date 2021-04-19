using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Example of an Singleton in Unity
 */
public class GameSettings : MonoBehaviour {
    public string message;
    public GameObject prefab;
    public CooldownManager cooldownManager;

    private static GameSettings instance;

    public PlayerMovement playerMovement;
    public Camera cam;
    public GameObject buildings;

    public static GameSettings Instance {
        get => instance;
    }

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Debug.LogWarning("GameSettings Object already exist.");
            Destroy(gameObject);
        }
    }
}