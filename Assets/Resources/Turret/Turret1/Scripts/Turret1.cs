using UnityEngine;
using System.Collections;

public class Turret1 : MonoBehaviour {

    public float LockSpeed = 1f;
    public float ShootSpeed = 1f;
    Transform head;
    GameObject targetObject;
    Animator anim;
    public GameObject bullet;
    float ShootLastTime = 0;
   // bool ReadyToShoot;
    DoorStatus DoorInfo;
    void Start() {
        anim = GetComponent<Animator>();
        head = transform.FindChild("Head");

    }


    void Update() {
        if (DoorInfo == DoorStatus.IsClosed) {
            if (head.rotation == Quaternion.identity)
                DoorInfo = DoorStatus.IsThePosition;
            HeadRotation(Quaternion.identity);
        } else if (DoorInfo == DoorStatus.IsOpened) {
            if (targetObject == null) {
                DoorInfo = DoorStatus.IsClosing;
                anim.SetBool("Open", false);
                return;
            }

            var dir = targetObject.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var newRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            HeadRotation(newRotation);
        }


    }


    void DoorInfoStatus(DoorStatus status) {
        DoorInfo = status;
    }


    void HeadRotation(Quaternion newRotation) {
        head.rotation = Quaternion.Slerp(head.rotation, newRotation, Time.deltaTime * LockSpeed);
        Vector3 v3 = head.TransformDirection(Vector3.up) * 10;
        // Debug.DrawRay(head.position, head.up * 100, Color.blue);
        // Debug.DrawLine(head.position + head.up, head.up * 100, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(head.position + head.transform.up, head.up * 100, 100, (1 << 9));
        if (hit.collider != null && hit.transform.tag == "Player") {
            if (bullet != null && (Time.fixedTime - ShootLastTime) > ShootSpeed) {
                ShootLastTime = Time.fixedTime;
                var currentBullet = Instantiate(bullet, head.position + head.up, head.rotation);
                Destroy(currentBullet, 5);
            }
        }

    }


    void OnTriggerEnter2D(Collider2D collaider) {
        if (collaider.tag == "Player") {
            anim.SetBool("Open", true);
            targetObject = collaider.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collaider) {
        if (collaider.tag == "Player") {
            anim.SetBool("Open", false);
            targetObject = null;
        }
    }

}
enum DoorStatus {
    IsOpened = 1,//xia
    IsClosed = 2,//daketilia
    IsThePosition = 5,
    IsOpenening = 4,//ixsneba
    IsClosing = 3//iketeba
}
