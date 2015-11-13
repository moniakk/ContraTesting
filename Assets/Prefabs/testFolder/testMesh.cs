using UnityEngine;
using System.Collections;

public class testMesh : MonoBehaviour {
    Mesh mesh;

    void Start() {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Update is called once per frame
    void Update() {
        var mesh_vertices = mesh.vertices;
        for (int i = 0; i < mesh.vertexCount; i++) {
            // mesh_vertices[i] = new Vector3(mesh_vertices[i].x + Random.Range(0f, 4f), mesh_vertices[i].y + Random.Range(0f, 4f), mesh_vertices[i].z + Random.Range(0f, 4f));
            mesh_vertices[i] = new Vector3(Random.Range(0f, 4f), Random.Range(0f, 4f), Random.Range(0f, 4f));
        }
        mesh.vertices = mesh_vertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

    }
}
