using UnityEngine;
using System.Collections;

public class Water4 : MonoBehaviour {


    Renderer render;

    public float speed = 1f;
    void Start() {
        render = GetComponent<Renderer>();

        render.material.SetFloat("_Speed", -speed);
    }

    // Update is called once per frame
    void Update() {

    }


    void OnDrawGizmos() {
        render = GetComponent<Renderer>();

        if (render.material != null) {// render.material.mainTextureScale = new Vector2(transform.localScale.x , transform.localScale.y);
                                      // render.material.mainTextureScale = new Vector2((transform.localScale.x / 60) * (1 / 12), 1);
            render.material.mainTextureScale = new Vector2((transform.localScale.x / 60) * (12.2f / transform.localScale.y), 1);
        }

    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            col.GetComponent<Rigidbody2D>().gravityScale = 0f; ;

        }
    }


    void OnTriggerExit2D(Collider2D col) {
        if (col.tag == "Player") {
            col.GetComponent<Rigidbody2D>().gravityScale = 3f; ;

        }
    }

    void OnTriggerStay2D(Collider2D col) {

        if (col.tag == "Player") {
            col.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed*25f, 1f));

        }
        //Bonus exercise. Fill in your code here for making things float in your water.
        //You might want to even include a buoyancy constant unique to each object!
    }
}
