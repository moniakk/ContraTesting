using UnityEngine;
using System.Collections;

public class FlyingAward1 : MonoBehaviour {
    public float SpeedX = 0.1f;
    public float RangeY = 0.1f;
    public float SpeedY = 0.1f;
    bool IsStarted = true;
    float CordY;
    void Start() {
        CordY = transform.position.y;
    }


    void Update() {
        if (IsStarted) {
            transform.position = new Vector3(transform.position.x + SpeedX, CordY + (Mathf.Sin(transform.position.x* SpeedY) * RangeY), transform.position.z);


        }
    }
}
