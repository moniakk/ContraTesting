using UnityEngine;
using System.Collections;

public class Water2 : MonoBehaviour
{

    Renderer render;
    Mesh mesh;
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        render = GetComponent<Renderer>();
        if (render.material != null)
        {
            //render.material.mainTextureScale = new Vector2(transform.localScale.x / transform.localScale.y, render.material.mainTextureScale.y);
            render.material.mainTextureScale = new Vector2(transform.localScale.x / transform.localScale.y, render.material.mainTextureScale.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3[] verticles = mesh.vertices;
        //for (int i = 0; i < mesh.vertexCount; i++)
        //{
        //    verticles[i] =new Vector3(Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2));
        //}
        //mesh.vertices = verticles;
        //mesh.RecalculateBounds();
        //mesh.RecalculateNormals();
        //GetComponent<MeshFilter>().sharedMesh = mesh;
    }

    void OnDrawGizmos()
    {
        render = GetComponent<Renderer>();

        if (render.material != null)
        {// render.material.mainTextureScale = new Vector2(transform.localScale.x , transform.localScale.y);
            render.material.mainTextureScale = new Vector2(transform.localScale.x / transform.localScale.y, render.material.mainTextureScale.y);
        }

    }

}
