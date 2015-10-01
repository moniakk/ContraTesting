using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class shoot : MonoBehaviour {
    public float speed = 0.1f;
   protected  Transform FirePoint;
    float dps = 3;

    private float lastShootTime;

    void FixedUpdate() {

        if(Input.GetMouseButton(0) && (Time.fixedTime - lastShootTime) > 1 / dps) {
            lastShootTime = Time.fixedTime;
            shootStart();
            }

        }

    void Start() {
        FirePoint = transform.FindChild("FirePoint");
        }
    public virtual void shootStart() {
     

        }

    }
