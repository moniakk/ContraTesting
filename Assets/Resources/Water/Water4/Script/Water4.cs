using UnityEngine;
using System.Collections;

public class Water4 : MonoBehaviour {

    public float Speed = .1f;
    Renderer render;
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }


    void OnDrawGizmos() {
        render = GetComponent<Renderer>();

        if (render.material != null) {// render.material.mainTextureScale = new Vector2(transform.localScale.x , transform.localScale.y);
                                      // render.material.mainTextureScale = new Vector2((transform.localScale.x / 60) * (1 / 12), 1);
           render.material.mainTextureScale = new Vector2((transform.localScale.x / 60) * (12.2f/ transform.localScale.y), 1);
        }

    }
}
