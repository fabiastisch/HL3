using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour {
    public Player player;

    public float maxDist = 100f;
    public float grabDistance = 8f;
    public float gravatyGunForce = 500;
    public float throwForce = 1000f;


    public LayerMask interactionLayer;

    private GameObject objInHand;

    private Camera cam;

    // Start is called before the first frame update
    void Start() {
        cam = player.cam;
    }

    // Update is called once per frame
    void Update() {
        // Grab and hold an GameObj, with which the player can interact || Gravitygun
        if (Input.GetButton("Fire1")) {
            if (!objInHand) {
                Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                Debug.DrawLine(ray.origin, ray.GetPoint(maxDist));

                RaycastHit raycastHit;
                if (Physics.Raycast(ray, out raycastHit, maxDist, interactionLayer)) {
                    GameObject hand = gameObject;
                    if (Vector3.Distance(hand.transform.position, raycastHit.transform.position) < grabDistance) {
                        objInHand = raycastHit.transform.gameObject;
                        objInHand.transform.position = hand.transform.position + cam.transform.forward;
                        objInHand.GetComponent<Rigidbody>().isKinematic = true;
                        objInHand.transform.parent = hand.transform;
                    }
                    else {
                        Vector3 dir = (hand.transform.position - raycastHit.transform.position).normalized;
                        raycastHit.transform.GetComponent<Rigidbody>()
                            .AddForce(dir * (gravatyGunForce * Time.deltaTime), ForceMode.VelocityChange);
                    }
                }
            }
        }

        if (Input.GetButtonDown("Fire2")) {
            if (objInHand) {
                objInHand.transform.parent = null;
                Rigidbody objHandRb = objInHand.GetComponent<Rigidbody>();
                objHandRb.isKinematic = false;
                objHandRb.AddForce(cam.transform.forward * throwForce);
                objInHand = null;
            }
        }
    }
}
