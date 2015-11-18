using UnityEngine;
using System.Collections;

public class Bird1 : MonoBehaviour {
    public float MaxDistance = 5f;
    Transform targetObject;

    Vector3 NewPosition;
    Vector3 StartPosition;
    bool IsStarted = true;
    bool LastPoint = true;
    public float RocketSpeed = 0.1f;

    void Start() {
        StartPosition = transform.position;
    
    }


    void Update() {
        if (IsStarted) {
            if (LastPoint) {
                if (targetObject != null) {
                    NewPosition = targetObject.transform.position;
                } else {
                    NewPosition = StartPosition + RandomVector3();
                }

                LastPoint = false;
            }

            var dir = NewPosition - transform.position;
            if (dir.x < 0) {
                transform.rotation = new Quaternion(0, 180, 0, 0);
            } else {
                transform.rotation = Quaternion.identity;
            }
            float distance = Vector3.Distance(transform.position, NewPosition);
            transform.position = Vector3.Lerp(transform.position, NewPosition, (.8f / distance) * RocketSpeed * Time.deltaTime);




            if (!LastPoint && distance < 1) {
                LastPoint = true;
            }
        }

        //if (targetObject != null) {

        //    Move(NewPosition);
        //} else {
        //    transform.Translate(Vector3.right * RocketSpeed * Time.deltaTime);
        //}


    }


    void Move(Vector3 NewPosition) {
        var dir = NewPosition - transform.position;
        if (dir.x < 0) {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        } else {
            transform.rotation = Quaternion.identity;
        }
        float distance = Vector3.Distance(transform.position, NewPosition);
        transform.position = Vector3.Lerp(transform.position, NewPosition, (.8f / distance) * RocketSpeed * Time.deltaTime);
    }

    Vector3 RandomVector3() {
        return new Vector3(Random.Range(-MaxDistance, MaxDistance), Random.Range(-MaxDistance, MaxDistance), 0);
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
