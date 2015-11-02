using UnityEngine;
using System.Collections;

public class Turret1 : MonoBehaviour {

    public float LockSpeed = 1f;
    public float DoorLock = 0f;
    Transform head;
    Transform doorLeft;
    Transform doorRight;
    GameObject targetObject;
    bool IsStarted;
    bool ReadyToShoot;
    void Start() {
        head = transform.FindChild("turretHead");
        doorLeft = transform.FindChild("doorLeft");
        doorRight = transform.FindChild("doorRight");
    }


    void Update() {
        if (IsStarted) {
            doorLeft.localPosition = new Vector2(DoorLock, 0);
            doorRight.localPosition = new Vector2(-DoorLock, 0);

            doorLeft.localScale = new Vector3(3 - DoorLock * 10, 5.37f, 1);
            doorRight.localScale = new Vector3(3 - DoorLock * 10, 5.37f, 1);


            head.localScale = new Vector3(1 + DoorLock, 1 + DoorLock, 1 + DoorLock);

        }
        if (ReadyToShoot) {
            var dir = targetObject.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var newRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            //  head.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            head.rotation = Quaternion.Slerp(head.rotation, newRotation, Time.deltaTime * LockSpeed);
            Vector3 v3 = head.TransformDirection(Vector3.up) * 10;
            Debug.DrawRay(head.position, head.up * 100, Color.blue);

        }

    }


    void OnTriggerEnter2D(Collider2D collaider) {
        if (collaider.tag == "Player") {
            targetObject = collaider.gameObject;
            IsStarted = true;
        }

    }
    void OnTriggerLeave2D(Collider2D collaider) {
        if (collaider.tag == "Player") {
            targetObject = null;
            IsStarted = false;
        }
    }

}
