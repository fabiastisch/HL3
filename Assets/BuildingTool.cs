using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BuildingTool : Tool {
    private Camera cam;
    public float maxBuildingDistance = 10f;
    public LayerMask buildableLayerMask;
    public GameObject[] frameworks;
    public GameObject[] buildings;
    private int frameworkIndex = 0;
    private GameObject currentFramework;


    private void Start() {
        cam = GameSettings.Instance.cam;
        Debug.Log("Start");
        Physics.IgnoreLayerCollision(0,8);
    }

    protected override void OnUpdate() {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) {
            Debug.Log("scrollUp");
            // scroll up
            frameworkIndex = frameworkIndex - 1 >= 0 ? frameworkIndex - 1 :
                this.frameworks.Length > 0 ? this.frameworks.Length - 1 : 0;
        }
        else if (scroll < 0f) {
            Debug.Log("scrollDown");
            // scroll down
            frameworkIndex = frameworkIndex + 1 < this.frameworks.Length ? frameworkIndex + 1 : 0;
        }

        if (Input.GetButtonDown("Fire1")) {
            if (currentFramework) {
                Debug.Log("Instantiate : " + frameworkIndex);
                Transform currentFrameworkTransform = currentFramework.transform;
                Destroy(currentFramework);
                currentFramework = null;
                Instantiate(this.buildings[frameworkIndex], currentFrameworkTransform.position, currentFrameworkTransform.localRotation).transform.parent = GameSettings.Instance.buildings.transform;
            }
            // replace Framework with building;
            // framework = null;
        }


        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawLine(ray.origin, ray.GetPoint(maxBuildingDistance));
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, maxBuildingDistance, buildableLayerMask)) {
            Debug.Log("RayCastHit");
            Vector3 hitPoint = raycastHit.point;
            hitPoint.x = Mathf.Round(hitPoint.x / 3) * 3;
            hitPoint.z = Mathf.Round(hitPoint.z/ 3) * 3;
            hitPoint.y = Mathf.Round(hitPoint.y/ 3) * 3;

            Vector3 direction = cam.transform.forward;
            /*if (direction.x < 0.5f && direction.x > -0.5f) {
                direction.z 
            }*/

            direction.y = 0;
            direction.x = Mathf.Round(direction.x);
            direction.z = direction.x == 1 || direction.x == -1 ? 0 : Mathf.Round(direction.z);

            currentFramework = Instantiate(this.frameworks[frameworkIndex], hitPoint, Quaternion.LookRotation(-direction));
            currentFramework.transform.parent = GameSettings.Instance.buildings.transform;
        }
        else {
            if (currentFramework) {
                Destroy(currentFramework);
            }
        }
    }

    private void OnDisable() {
        if (currentFramework) {
            Destroy(currentFramework);
        }
    }
}