using UnityEngine;
using System.Collections;

public class Rocket2 : MonoBehaviour {

    public Transform targetObject;


    public float RocketSpeed = 0.1f;
    [Range(0, 1)]
    public float MaxAngle = 0.05f;
    void Start() {

    }


    void Update() {
        if (targetObject != null) {
            var dir = targetObject.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var newRotation = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * MaxAngle * RocketSpeed);

        }
        transform.Translate(Vector3.right * RocketSpeed * Time.deltaTime);
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
