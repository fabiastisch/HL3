using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Tool {
    private Camera cam;

    private float maxDist = 100f;

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
            if (Physics.Raycast(ray, out raycastHit, maxDist, interactionLayer)) {
                GameObject hittedObj = raycastHit.transform.gameObject;
                Health healthScript = hittedObj.GetComponent<Health>();
                if (healthScript) {
                 healthScript.Attack(damage);   
                }
            }
        }
    }
}
