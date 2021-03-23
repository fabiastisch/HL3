using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour {
    public GameObject[] frameworks;

    private int frameworkIndex = 0;
    private GameObject framework;
    public LayerMask builingLayer;
    private bool buildingMode = false;
    public Material transparent;

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

    public float grabDistance;
    public float gravatyGunForce;


    private void FixedUpdate() {
        if (buildingMode) {
            

            if (framework) {
                Destroy(framework);
                // TODO: not working fine
                framework.layer = LayerMask.NameToLayer("Building");
                framework = null;
            }

            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            Debug.DrawRay(ray.origin, ray.GetPoint(maxDist));
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, maxDist, builingLayer)) {
                Vector3 hitPoint = raycastHit.point;
                hitPoint.x = Mathf.Round(hitPoint.x);
                hitPoint.z = Mathf.Round(hitPoint.z);
                hitPoint.y = Mathf.Round(hitPoint.y);

                Vector3 direction = cam.transform.forward;
                /*if (direction.x < 0.5f && direction.x > -0.5f) {
                    direction.z 
                }*/

                direction.y = 0;
                direction.x = Mathf.Round(direction.x);
                direction.z = direction.x == 1 || direction.x == -1 ? 0 : Mathf.Round(direction.z);

                Debug.Log(direction);

                framework = Instantiate(this.frameworks[frameworkIndex], hitPoint, Quaternion.LookRotation(direction));
            }

            return;
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            buildingMode = !buildingMode;
        }

        if (buildingMode) {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0f) {
                // scroll up
                frameworkIndex = frameworkIndex - 1 >= 0 ? frameworkIndex - 1 :
                    this.frameworks.Length > 0 ? this.frameworks.Length - 1 : 0;
            }
            else if (scroll < 0f) {
                // scroll down
                frameworkIndex = frameworkIndex + 1 < this.frameworks.Length ? frameworkIndex + 1 : 0;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                framework = null;
            }
        }
        else {
            DoOldStuff();
        }

    }

    private void DoOldStuff() {
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
        if (Input.GetKey(KeyCode.E)) {
            Instantiate(boxPrefab, hand.transform.position, Quaternion.identity);
        }

        // Throw a ball
        if (Input.GetKey(KeyCode.Q)) {
            GameObject ball = Instantiate(ballPrefab, hand.transform.position, Quaternion.identity);
            Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
            rigidbody.AddForce(transform.forward * throwForce);
        }

        // Grab and hold an GameObj, with which the player can interact || Gravitygun
        if (Input.GetKey(KeyCode.Mouse1)) {
            if (!objInHand) {
                Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                Debug.DrawLine(ray.origin, ray.GetPoint(maxDist));

                RaycastHit raycastHit;
                if (Physics.Raycast(ray, out raycastHit, maxDist, interactionLayer)) {
                    if (Vector3.Distance(hand.transform.position, raycastHit.transform.position) < grabDistance) {
                        objInHand = raycastHit.transform.gameObject;
                        objInHand.transform.position = hand.transform.position;
                        objInHand.GetComponent<Rigidbody>().isKinematic = true;
                        objInHand.transform.parent = hand.transform;
                    }
                    else {
                        Vector3 dir = (hand.transform.position - raycastHit.transform.position).normalized;
                        raycastHit.transform.GetComponent<Rigidbody>().AddForce(dir * (gravatyGunForce * Time.deltaTime), ForceMode.VelocityChange);
                    }
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