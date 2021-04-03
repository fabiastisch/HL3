using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Building : MonoBehaviour {
    public LayerMask buildableMask;
    private List<GameObject> groundPath = new List<GameObject>();
    private Vector3 center;
    private static int notGroundedCounter = 0;
    private bool Checkedgrounded = false;
    private static List<Building> currentlyChecked = new List<Building>();

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
        // Debug.Log("Center: " + center);
        Collider[] colliders = Physics.OverlapSphere(center, 2.3f, buildableMask);
        Debug.Log("Initial Found Neighbours: " + colliders.Length);
        foreach (var neighbour in colliders) {
            // check for every neighbour if is grounded
            Building building = neighbour.gameObject.GetComponent<Building>();
            if (building) {
                // only if the neighbourObj have an Building script.
                building.CheckGroundPathFromDestroyedObj1(gameObject);
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

    /**
     * Remove this obj if its not Grounded.
     */
    private void CheckGroundPathFromDestroyedObj1(GameObject o) {
        Vector3 pos = o.gameObject.GetComponent<Building>().GetCenter();
        // Debug.Log("Center: " + pos + "\n on " + GetComponent<Renderer>().bounds.center );
        Debug.DrawLine(pos, GetComponent<Renderer>().bounds.center, Color.cyan, float.MaxValue);
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
               grounded = building.FindGround(gameObject);
            }
        }
        
        currentlyChecked.Clear();
        if (!grounded) {
            destroyAfterSeconds(0.7f);
        }


        /*foreach (var neighbour in colliders) {
            Building building = neighbour.gameObject.GetComponent<Building>();
            if (neighbour.gameObject == o) {
                continue;
            }

            if (building) {
                
            }
            else {
                // is grounded
            }
        }*/
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

    public void isGrounded(GameObject o) {
        Vector3 pos = o.gameObject.GetComponent<Building>().GetCenter();
        Collider[] colliders = Physics.OverlapSphere(center, 2.3f, buildableMask);
        bool grounded = false;
        foreach (var neighbour in colliders) {
            Building building = neighbour.gameObject.GetComponent<Building>();
            if (neighbour.gameObject == o) {
                continue;
            }

            if (building) {
                // only if the neighbourObj have an Building script.
                //  bool temp = building.CheckGroundPath(gameObject, index +1);
                //  grounded = temp ? temp : grounded;
            }
            else {
                grounded = true;
            }
        }
    }

    public bool CheckGroundPath(GameObject o, int index) {
        if (index > 2) {
            //TODO Fix: Optimize Algorithm
            Debug.Log("Index to high: " + index);
            return false;
        }

        //  Debug.Log("CheckGroundPath");
        Vector3 pos = o.gameObject.GetComponent<Building>().GetCenter();
        // Debug.Log("Center: " + pos + "\n on " + GetComponent<Renderer>().bounds.center );
        Debug.DrawLine(pos, GetComponent<Renderer>().bounds.center, Color.cyan, float.MaxValue);
        Collider[] colliders = Physics.OverlapSphere(center, 2.3f, buildableMask);
        Debug.Log("Found Neighbours: " + colliders.Length);
        bool grounded = false;
        foreach (var neighbour in colliders) {
            Building building = neighbour.gameObject.GetComponent<Building>();
            if (neighbour.gameObject == o) {
                continue;
            }

            if (building) {
                // only if the neighbourObj have an Building script.
                bool temp = building.CheckGroundPath(gameObject, index + 1);
                grounded = temp ? temp : grounded;
            }
            else {
                grounded = true;
            }
        }

        if (grounded) {
            return grounded;
        }
        else {
            // Invoke(nameof(destory),10f);
            Debug.Log("NotGrounded Objs: " + ++notGroundedCounter);
            return grounded;
        }
    }

    private void destory() {
        Destroy(gameObject);
    }

    public void destroyAfterSeconds(float time) {
        Invoke(nameof(destory), time);
    }

    /**
     * Check if this GameObject must be destoyed, cause GameObject o got destoryed.
     * 
     */
    public void CheckGroundPathFromDestroyedObj(GameObject o) {
        // Debug.Log("CheckGroundPath");
        Vector3 pos = o.gameObject.GetComponent<Building>().GetCenter();
        // Debug.Log("Center: " + pos + "\n on " + GetComponent<Renderer>().bounds.center );
        Debug.DrawLine(pos, GetComponent<Renderer>().bounds.center, Color.cyan, float.MaxValue);

        Collider[] colliders = Physics.OverlapSphere(center, 2.3f, buildableMask);
        Debug.Log("Found Neighbours: " + colliders.Length);
        bool grounded = false;
        foreach (var neighbour in colliders) {
            Building building = neighbour.gameObject.GetComponent<Building>();
            if (neighbour.gameObject == o) {
                continue;
            }

            if (building) {
                // only if the neighbourObj have an Building script.
                bool temp = building.CheckGroundPath(gameObject, 0);
                if (!temp) {
                    building.destroyAfterSeconds(0.5f);
                }

                grounded = temp ? temp : grounded;
            }
            else {
                grounded = true;
            }
        }

        if (grounded) {
            Debug.Log("Grounded Obj found");
        }
        else {
            destroyAfterSeconds(1f);
        }
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