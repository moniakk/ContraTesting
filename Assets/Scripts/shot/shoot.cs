﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class shoot : MonoBehaviour {
    public float speed = 0.1f;

    float dps = 3;

    private float lastShootTime;

    void FixedUpdate() {

        if(Input.GetMouseButtonDown(0) && (Time.fixedTime - lastShootTime) > 1 / dps) {
            lastShootTime = Time.fixedTime;

            shootStart();
            }
        }

    public virtual void shootStart() {


        }

    }
