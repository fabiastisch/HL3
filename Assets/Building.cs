using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Building : MonoBehaviour {
    public LayerMask buildableMask;
    private Vector3 center;
    private static List<Building> currentlyChecked = new List<Building>();

    // Start is called before the first frame update
    void Start() {
        center = GetComponent<Renderer>().bounds.center;
    }

    private void OnDrawGizmos() {
        /*Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(GetComponent<Renderer>().bounds.center, 2.3f);*/
    }

    /**
     * This Method will be called, when this GameObject get Destroyed. 
     */
    private void OnDestroy() {
        Debug.Log("OnDestory");
        // Say every neighbour that this object will be destoryed.
        // Debug.Log("Center: " + center);
        Collider[] colliders = Physics.OverlapSphere(center, 2.3f, buildableMask);
        Debug.Log("Initial Found Neighbours: " + colliders.Length);
        foreach (var neighbour in colliders) {
            // check for every neighbour if is grounded
            Building building = neighbour.gameObject.GetComponent<Building>();
            if (building) {
                // only if the neighbourObj have an Building script.
                building.CheckGroundPathFromDestroyedObj(gameObject);
            }
        }
    }

    /**
     * Remove this obj if its not Grounded.
     */
    private void CheckGroundPathFromDestroyedObj(GameObject o) {
        Vector3 pos = o.gameObject.GetComponent<Building>().GetCenter();
        // Debug.Log("Center: " + pos + "\n on " + GetComponent<Renderer>().bounds.center );
        Debug.DrawLine(pos, GetComponent<Renderer>().bounds.center, Color.yellow, float.MaxValue);
        Collider[] colliders = Physics.OverlapSphere(center, 2.3f, buildableMask);
        bool grounded = false;
        List<Building> buildings = new List<Building>();
        foreach (var neighbour in colliders) {
            Building building = neighbour.gameObject.GetComponent<Building>();
            if (!building) {
                grounded = true;
                break;
            }
            else {
                buildings.Add(building);
            }
        }

        if (!grounded) {
            currentlyChecked.Add(this);
            // If this is not grounded, then check neighbours
            foreach (Building building in buildings) {
                // building.checkGrounded(gameObject);
               bool temp = building.FindGround(gameObject);
               grounded = temp ? temp : grounded;
            }
        }
        
        currentlyChecked.Clear();
        if (!grounded) {
            destroyAfterSeconds(0.7f);
        }
        
    }

    private bool FindGround(GameObject o) {
        Collider[] colliders = Physics.OverlapSphere(center, 2.3f, buildableMask);
        bool grounded = false;
        List<Building> buildings = new List<Building>();
        foreach (var neighbour in colliders) {
            Building building = neighbour.gameObject.GetComponent<Building>();
            if (!building) {
                // grounded = true;
                // break;
                return true;
            }

            buildings.Add(building);
        }
        foreach (Building checkedBuilding in currentlyChecked) {
            buildings.Remove(checkedBuilding);
        }
        currentlyChecked.Add(this);
        // If this is not grounded, then check neighbours
        foreach (Building building in buildings) {
            // building.checkGrounded(gameObject);
            bool temp = building.FindGround(gameObject);
            grounded = temp ? temp : grounded;
        }
        return grounded;
    }
    

    private void destory() {
        Destroy(gameObject);
    }

    public void destroyAfterSeconds(float time) {
        Invoke(nameof(destory), time);
    }
    
    public Vector3 GetCenter() {
        return center;
    }
}