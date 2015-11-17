using UnityEngine;
using System.Collections;

public class Fish1 : MonoBehaviour {
    public float MaxDistance = 5f;
    public float Speed = 1f;
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

            int tmp = gameObject.GetInstanceID();

            var dir = NewPosition - transform.position;
            if (dir.x < 0) {
                transform.rotation = new Quaternion(0, 180, 0, 0);
            } else {
                transform.rotation = Quaternion.identity;
            }
            float distance = Vector3.Distance(transform.position, NewPosition);
            transform.position = Vector3.Lerp(transform.position, NewPosition, (.8f / distance) * _Speed * Time.deltaTime);


            if (!LastPoint && distance < 1) {
                LastPoint = true;
            }
        }
    }


    void Move1() {

    }

    void Move(Vector3 NewPosition) {

        var dir = NewPosition - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var newRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 1 * _Speed);
        transform.Translate(Vector3.right * _Speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D Hit) {
        if (Hit.tag == "Water") {
            _Speed = Speed;
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
