using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
    public LayerMask buildableMask;
    private List<GameObject> groundPath = new List<GameObject>();
    private Vector3 center;

    // Start is called before the first frame update
    void Start() {
        center = GetComponent<Renderer>().bounds.center;
        Collider[] colliders = Physics.OverlapSphere(center, 2.3f, buildableMask);
        foreach (var collider in colliders) {
            groundPath.Add(collider.gameObject);
            //Debug.Log(collider.gameObject.name);
        }  
    }

    // Update is called once per frame
    void Update() {
        /*Vector3 center = GetComponent<Renderer>().bounds.center;
        Debug.DrawLine(GameSettings.Instance.cam.transform.position, center);
        

        if (groundPath.Count == 0) {
            Debug.Log("Destroy");
            Destroy(gameObject);
        }*/

        /*if (colliders.Length <= 1) {
            Destroy(gameObject);
        }*/
    }

    private void OnDrawGizmos() {
        /*Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(GetComponent<Renderer>().bounds.center, 2.3f);*/
    }

    private void OnDestroy() {
        Debug.Log("OnDestory");
        // Say every neighbour that this object will be destoryed.
        Debug.Log("Center: " + center);
        Collider[] colliders = Physics.OverlapSphere(center, 2.3f, buildableMask);
        Debug.Log("Found Neighbours: " +colliders.Length);
        foreach (var neighbour in colliders) {
            Building building = neighbour.gameObject.GetComponent<Building>();
            if (building) { // only if the neighbourObj have an Building script.
                building.CheckGroundPath(gameObject);
            }
        }
        
        /*Vector3 center = GetComponent<Renderer>().bounds.center;
        Collider[] colliders = Physics.OverlapSphere(center, 2.3f, buildableMask);
        Debug.Log(colliders.Length);
        bool grounded = false;
        foreach (var collider in colliders) {
            Building building= collider.gameObject.GetComponent<Building>();
            if (building) {
                grounded = grounded != false || building.checkGrounded(gameObject);
            }
        }

        if (!grounded) {
            Debug.Log("Destory all? Count: " +colliders.Length);
            foreach (var collider in colliders) {
                Building building= collider.gameObject.GetComponent<Building>();
                if (building) {
                    building.DestroyList();
                }
                else {
                    Debug.Log("noBuilding");
                }
                // Destroy(collider.gameObject);
            }  
        }*/
    }

    public void CheckGroundPath(GameObject o) {
        Debug.Log("CheckGroundPath");
        Vector3 pos = o.gameObject.GetComponent<Building>().GetCenter();
        Debug.Log("Center: " + pos + "\n on " + GetComponent<Renderer>().bounds.center );
        Debug.DrawLine(pos, GetComponent<Renderer>().bounds.center, Color.cyan, float.MaxValue);
    }

    public bool checkGrounded(GameObject obj) {
        // groundPath.Remove(obj);
       //  Debug.Log("CheckGround + " + groundPath.Count);
        bool grounded = false;
        foreach (var groundObj in groundPath) { 
            if (groundObj == obj || !groundObj) {
                continue;
            }
            Building building = groundObj.GetComponent<Building>();
            if (building == null) {
                Debug.Log("GroundFond");
                return true;
            }
            grounded = grounded != false || building.checkGrounded(gameObject);
            
        }

        return grounded;
    }

    public void DestroyList() {
        Debug.Log("DestroyList");
        foreach (var groundObj in groundPath) { 
            if (!groundObj) {
                continue;
            }
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }

    public Vector3 GetCenter() {
        return center;
    }
}