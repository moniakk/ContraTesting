using UnityEngine;
using System.Collections;

public class Bird1 : MonoBehaviour {

    public Transform targetObject;
    bool IsStarted = false;

    public float RocketSpeed = 0.1f;




    void Update() {
        if (true) {

        }
        if (targetObject != null) {
            var dir = targetObject.position - transform.position;
            if (dir.x < 0) {
                transform.rotation = new Quaternion(0, 180, 0, 0);
            } else {
                transform.rotation = Quaternion.identity;
            }
            float distance = Vector3.Distance(transform.position, targetObject.position);
            transform.position = Vector3.Lerp(transform.position, targetObject.position, (.8f / distance) *RocketSpeed* Time.deltaTime);
        } else {
            transform.Translate(Vector3.right * RocketSpeed * Time.deltaTime);
        }


    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player") {
            targetObject = collider.transform;
        }

    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
