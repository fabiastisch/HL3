using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour {
    private Camera cam;

    private float maxDist = 100f;

    public int damage = 10;
    public LayerMask interactionLayer;
    
    // Start is called before the first frame update
    void Start() {
        cam = GameSettings.Instance.cam;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButton("Fire1")) {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            Debug.DrawLine(ray.origin, ray.GetPoint(maxDist));

            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, maxDist, interactionLayer)) {
                GameObject hittedObj = raycastHit.transform.gameObject;
                Life lifeScript = hittedObj.GetComponent<Life>();
                if (lifeScript) {
                 lifeScript.Attack(damage);   
                }
            }
        }
    }
}