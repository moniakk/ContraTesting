using UnityEngine;
using System.Collections;

public class Fish1 : MonoBehaviour {
    float MaxDistance = 5f;
    float Speed = 1f;
    float _Speed;
    bool IsStarted;

    bool Agresive = true;
    Vector3 NewPosition;
    Vector3 StartPosition;
    bool LastPoint = true;
    

    void Start() {
        StartPosition = transform.position;
        _Speed = Speed;
    }


    void Update() {
        if (IsStarted) {
            if (LastPoint) {
                NewPosition = StartPosition + new Vector3(Random.Range(-MaxDistance, MaxDistance), Random.Range(-MaxDistance, MaxDistance), 0);
                LastPoint = false;
            }



            var dir = NewPosition - transform.position;


            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var newRotation = Quaternion.AngleAxis(angle, Vector3.forward);


            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 1 * _Speed);


            transform.Translate(Vector3.right * _Speed * Time.deltaTime);

            if (Vector3.Distance(NewPosition, transform.position) < 1) {
                LastPoint = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D Hit) {
        if (Hit.tag == "Water") {
            _Speed = Speed ;
        }
    }
    void OnTriggerExit2D(Collider2D Hit) {
        if (Hit.tag == "Water") {
            NewPosition = StartPosition;
            _Speed = Speed * 5;

        }
    }

    void OnBecameInvisible() {
        IsStarted = false;
    }
    void OnBecameVisible() {
        IsStarted = true;
    }
}
