using UnityEngine;

public class Utils {
    public static float getDistanceBetweenGameObjects(GameObject g1, GameObject g2) {
        return Vector3.Magnitude(g1.transform.position - g2.transform.position);
    }

    public static Mesh CopyMesh(Mesh mesh) {
        Mesh newMesh = new Mesh();
        newMesh.vertices = mesh.vertices;
        newMesh.triangles = mesh.triangles;
        newMesh.uv = mesh.uv;
        newMesh.normals = mesh.normals;
        newMesh.colors = mesh.colors;
        newMesh.tangents = mesh.tangents;
        return newMesh;
    }
    public static Vector3 rotateRotation(Vector3 one, Vector3 two) {
        return new Vector3(one.x + two.x, one.y + two.y, one.z + two.z);
    }
}