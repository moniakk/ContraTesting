using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
namespace Bot {
    public class Bot3 : Bot {


        public bool AutoTargetPlayer;
        public bool Loop;
        public Vector3 EndPosition;
        Vector3 startPosition;
        Vector3 TargetPosition;
        Rigidbody2D rigidbody2D;
        bool IsJumping;
        bool IsStarted;

        List<Collider2D> colaiders = new List<Collider2D>();
        Collider2D cuurentColaider;


        void Start() {
            rigidbody2D = GetComponent<Rigidbody2D>();
            startPosition = transform.position;
            EndPosition = startPosition + EndPosition;
            if (!AutoTargetPlayer)
                TargetPosition = EndPosition;
        }


        void Update() {
            if (IsStarted) return;

            if (!AutoTargetPlayer) {
                // MoveToTarget();

            }
            if (Input.GetMouseButtonDown(1)) {
                jumpTest();
                foreach (var item in colaiders) {
                    if (item != cuurentColaider) {
                        //  Jump(item.transform.position);
                        break;
                    }

                }
            }

        }




        void MoveToTarget() {
            Vector3 nextPosition = TargetPosition - transform.position;
            if (Convert.ToInt32(EndPosition.x - transform.position.x) == 0) {
                TargetPosition = startPosition;
            } else if (Convert.ToInt32(startPosition.x - transform.position.x) == 0) {
                TargetPosition = EndPosition;
            }

            Move(nextPosition.normalized);
        }

        void Move(Vector3 translation) {
            if (IsJumping) {
                transform.rotation = translation.x > 0 ? new Quaternion(0, 180, 0, 1) : new Quaternion(0, 0, 0, 1);
                Vector3 newPosition = transform.position + (translation * movementSpeed);
                transform.position = new Vector3(newPosition.x, transform.position.y, 0);
                // transform.Translate(translation.x * movementSpeed, 0, 0);
            }

        }

        void Jump(Vector3 target) {
            float yTar = target.y - transform.position.y;
            float xTar = target.x - transform.position.x;
            float distance = Vector3.Distance(transform.position, target);

            rigidbody2D.AddForce(new Vector2(31 * xTar, 150 * Math.Abs(yTar)));



        }
        void jumpTest() {

            Vector2 force = new Vector2(1, 3);
            Vector2 Gravity = new Vector2(0f, -1f);

            for (int i = 0; i < 100; i++) {
                Vector3 pos = new Vector3(force.x, force.y) * 0.01f;
                force = force - Gravity;
                transform.position = pos;
            }

        }



        void OnCollisionEnter2D(Collision2D col) {
            if (col.transform.tag == "floor") {
                cuurentColaider = col.collider;
            }
            IsJumping = true;
        }



        void OnTriggerEnter2D(Collider2D col) {
            if (col.tag == "Player") {
                IsStarted = true;
            }
            if (col.tag == "floor") {
                colaiders.Add(col);
            }

        }

        void OnTriggerLeave2D(Collider2D col) {
            colaiders.Remove(col);
        }



    }
}
