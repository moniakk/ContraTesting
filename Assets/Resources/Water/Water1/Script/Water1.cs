using UnityEngine;
using System.Collections;

public class Water1 : MonoBehaviour {
    public float speed = 0.1f;
    public float Position, Velocity;

    public Renderer body;
    void Start() {
        body = transform.FindChild("Body").GetComponent<Renderer>();
    }


    void Update() {

        body.material.mainTextureOffset = new Vector2(Time.time * speed, 0);
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
}
