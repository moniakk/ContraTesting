using UnityEngine;
using System.Collections;

public class FlyingPlane1 : MonoBehaviour {
    public float SpeedX = 0.1f;
    public float RangeY = 0.1f;
    public float SpeedY = 0.1f;
    public float ShootSpeed = 1f;
    public Transform bullet;
    bool IsStarted = true;
    float lastShootTime;
    float CordY;
    void Start() {
        CordY = transform.position.y;
    }


    void Update() {
        if (IsStarted) {
            transform.position = new Vector3(transform.position.x + SpeedX, CordY + (Mathf.Sin(transform.position.x * SpeedY) * RangeY), transform.position.z);
            if (bullet != null && (Time.fixedTime - lastShootTime) > ShootSpeed) {
                lastShootTime = Time.fixedTime;
                var NewBomb = Instantiate(bullet, transform.position + Vector3.down, Quaternion.identity);
               // Destroy(NewBomb, 15);
            }

        }
    }
}
