using UnityEngine;
using System.Collections;

public class AutoRepeatMaterialTiling : MonoBehaviour {

    public float TilingX = 1f;
    public float TilingY = 1f;
    Renderer render;

    void OnDrawGizmos()
    {
        render = GetComponent<Renderer>();

        if (render != null)
        {
            render.material.mainTextureScale = new Vector2(transform.localScale.x / TilingX, transform.localScale.y / TilingY);
        }

    }
}
