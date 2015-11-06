using UnityEngine;
using System.Collections;

public class Cart1 : MonoBehaviour {
    public float Speed = 0.1f;
    bool IsStarted;

    GameObject targetObject;
    void Start() {

    }


    void Update() {
        if (IsStarted) {

            if (targetObject != null) {
           //     targetObject.transform.Translate(new Vector3(Speed, 0));
            }
          //  transform.Translate(new Vector3(Speed, 0));


        }

    }
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.tag == "Player") {
            IsStarted = true;
            //Vector2 test = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
          //   transform.GetComponent<Rigidbody2D>().velocity = new Vector2(test.x*10,0);
            targetObject = collision.gameObject;
        }


    }
    void OnCollisionExit2D(Collision2D collision) {
        if (collision.transform.tag == "Player") {
            // IsStarted = false;
            targetObject = null;
        }


    }
}
