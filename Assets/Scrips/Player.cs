using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    /**
     * Reference to a Box.
     */
    public GameObject boxPrefab;
    
    /**
     * Reference to the Hand.
     */
    public GameObject hand;

    /**
     * Reference to the Ball.
     */
    public GameObject ballPrefab;

    /**
     * Reference to the Bomb
     */
    public GameObject bombPrefab;

    /**
     * The Force value that is used to throw Object.
     */
    public float throwForce;

    /**
     * Reference to the InteractionLayerMask.
     */
    public LayerMask interactionLayer;

    /**
     * Reference to the Camera.
     */
    public Camera cam;

    /**
     * The maximum distance that allows the player to interact with the environment. 
     */
    public float maxDist;

    /**
     * Used to store the GameObject that this player is currently holding.
     */
    private GameObject objInHand;
    
    
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.I)) {
            Instantiate(GameSettings.Instance.prefab, hand.transform.position, Quaternion.identity);
        }

        // Slow down time
        if (Input.GetKeyDown(KeyCode.T)) {
            if (Time.timeScale == 1f) {
                Time.timeScale = 0.25f;
            }
            else {
                Time.timeScale = 1;
            }
        }

        // Instantiate a Box
        if (Input.GetKeyDown(KeyCode.E)) {
            Instantiate(boxPrefab, hand.transform.position, Quaternion.identity);
        }

        // Throw a ball
        if (Input.GetKeyDown(KeyCode.Q)) {
            GameObject ball = Instantiate(ballPrefab, hand.transform.position, Quaternion.identity);
            Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
            rigidbody.AddForce(transform.forward * throwForce);
        }

        // Grab and hold an GameObj, with which the player can interact
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            Debug.DrawLine(ray.origin, ray.GetPoint(maxDist));

            if (!objInHand) {
                RaycastHit raycastHit;
                if (Physics.Raycast(ray, out raycastHit, maxDist, interactionLayer)) {
                    objInHand = raycastHit.transform.gameObject;
                    objInHand.transform.position = hand.transform.position;
                    objInHand.GetComponent<Rigidbody>().isKinematic = true;
                    objInHand.transform.parent = hand.transform;
                }
            }
        }

        // Either throw the holding GameObj, or throw a bomb
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            if (objInHand) {
                objInHand.transform.parent = null;
                Rigidbody objHandRb = objInHand.GetComponent<Rigidbody>();
                objHandRb.isKinematic = false;
                objHandRb.AddForce(cam.transform.forward * throwForce);
                objInHand = null;
            }
            else {
                GameObject ball = Instantiate(bombPrefab, hand.transform.position, Quaternion.identity);
                Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
                rigidbody.AddForce(transform.forward * throwForce);
            }
        }
    }
}