using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Building : MonoBehaviour {
    public LayerMask buildableMask;
    public GameObject collisionBox;
    private Vector3 center;
    private static List<Building> currentlyChecked = new List<Building>();

    private float shpereRadius = 2f;
    private Vector3 halfExtents = new Vector3(1.5f,1f,1.5f);

    public Vector3 rotation = Vector3.zero;
    private Quaternion colliderRotation;

    public float ScaleX = 1.0f;
    public float ScaleY = 1.0f;
    public float ScaleZ = 1.0f;
    public bool RecalculateNormals = false;
    private Vector3[] _baseVertices;
    // Start is called before the first frame update
    void Start() {
        center = GetComponent<Renderer>().bounds.center;
        
    }
    public void SetColliderRotation(Quaternion rotation) {
        colliderRotation = rotation;
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        // Mesh mesh = GetComponent<MeshCollider>().sharedMesh;
        Mesh mesh = Utils.CopyMesh(GetComponent<MeshFilter>().sharedMesh);
        Debug.Log(mesh);
        if (_baseVertices == null)
            _baseVertices = mesh.vertices;
        var vertices = new Vector3[_baseVertices.Length];
        for (var i = 0; i < vertices.Length; i++)
        {
            var vertex = _baseVertices[i];
            vertex.x = vertex.x * ScaleX;
            vertex.y = vertex.y * ScaleY;
            vertex.z = vertex.z * ScaleZ;
            vertices[i] = vertex;
        }
        mesh.vertices = vertices;
        if (RecalculateNormals)
            mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        // Gizmos.DrawSphere(GetComponent<Renderer>().bounds.center, shpereRadius);
        // Gizmos.DrawMesh(mesh, center);
    }

    /**
     * This Method will be called, when this GameObject get Destroyed. 
     */
    private void OnDestroy() {
        Debug.Log("OnDestory");
        // Say every neighbour that this object will be destoryed.
        // Debug.Log("Center: " + center);
        Collider[] colliders = GetCollider();
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
        Collider[] colliders = GetCollider();
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

    private Collider[] GetCollider() {
        ExtDebug.DrawBox(center,new Vector3(1.6f,0.6f,2.2f),Quaternion.Euler(Utils.rotateRotation(rotation, colliderRotation.eulerAngles)),Color.red, 10f);
        return Physics.OverlapBox(center, new Vector3(1.6f,0.6f,2.2f), Quaternion.Euler(Utils.rotateRotation(rotation, colliderRotation.eulerAngles)));
        
        /*Physics.OverlapBox(center, halfExtents, Quaternion.identity);
        
        return Physics.OverlapSphere(center, shpereRadius, buildableMask);*/
    }

    private bool FindGround(GameObject o) {
        Collider[] colliders = GetCollider();
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