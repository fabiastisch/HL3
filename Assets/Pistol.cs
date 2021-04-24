using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

public class Pistol : Tool {
    private Camera cam;
    public GameObject bullet;

    public Transform bulletPoint;
    public float maxDist = 1000;

    public float bulletForce = 100 * 100 * 15;

    public float damage = 10;
    public LayerMask interactionLayer;

    // Start is called before the first frame update
    void Start() {
        cam = GameSettings.Instance.cam;
    }

    // Update is called once per frame
    void Update() {
        if (!photonView.IsMine) {
            return;
        }

        if (Input.GetButtonDown("Fire1")) {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            Debug.DrawLine(ray.origin, ray.GetPoint(maxDist));

            RaycastHit raycastHit;
            RaycastHit[] raycastHits = Physics.RaycastAll(ray, maxDist, interactionLayer);
            // if (Physics.Raycast(ray, out raycastHit, maxDist, interactionLayer)) {
            Array.Sort(raycastHits, (a, b) => a.distance.CompareTo(b.distance));

            GameObject hittedObj = GetHittedObject(raycastHits);
            if (hittedObj) {
                Debug.Log(hittedObj);
                IAttackable healthScript = hittedObj.GetComponent(typeof(IAttackable)) as IAttackable;

                if (healthScript != null) {
                    Debug.Log("HealthScript: " + healthScript);
                    healthScript.Attack(damage);
                }
            }

            /*GameObject hittedObj = raycastHit.transform.gameObject;
            Debug.Log(hittedObj);
            IAttackable healthScript = hittedObj.GetComponent(typeof(IAttackable)) as IAttackable;
            Debug.Log("HealthScript: "+healthScript);
            if (healthScript != null) {
             healthScript.Attack(damage);   
            }*/
            // }
            /*GameObject bulletInstance = PhotonNetwork.Instantiate(bullet.name, bulletPoint.position, bulletPoint.rotation, 0);
            bulletInstance.GetComponent<Rigidbody>().AddForce(cam.transform.forward * bulletForce);
            bulletInstance.GetComponent<BulletScript>().Owner = GameSettings.Instance.playerMovement.gameObject;*/
        }
    }

    private GameObject GetHittedObject(RaycastHit[] raycastHits) {
        if (raycastHits.Length == 0) {
            return null;
        }

        GameObject hitted = raycastHits[0].transform.gameObject;
        if (hitted.Equals(GameSettings.Instance.playerMovement.gameObject)) {
            return GetHittedObject(raycastHits.Skip(1).ToArray());
        }

        return hitted;
    }
}