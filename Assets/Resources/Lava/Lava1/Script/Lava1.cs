using UnityEngine;
using System.Collections;

public class Lava1 : MonoBehaviour {
    
    public Vector2 uvAnimationRate = new Vector2(1.0f, 0.0f);
    public string textureName = "_MainTex";
    Renderer renderer;
    Vector2 uvOffset = Vector2.zero;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void LateUpdate()
    {
        uvOffset += (uvAnimationRate * Time.deltaTime);
        if (renderer.enabled)
        {
            renderer.sharedMaterial.SetTextureOffset(textureName, uvOffset);
        }
    }
    void OnDrawGizmos()
    {
        renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            renderer.sharedMaterials[0].mainTextureScale = new Vector2(transform.localScale.x /10, transform.localScale.y / 10);
        }

    }
}
