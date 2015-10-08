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
        bool IsJumping = true;
        bool IsStarted;

        List<Collider2D> colaiders = new List<Collider2D>();
        Collider2D cuurentColaider;

        List<Collider2D> jumpColaider;

        void Start() {
            jumpColaider = transform.FindChild("JumpColaider").GetComponent<JumpColaider>().colaiders;
           
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
                foreach (var item in colaiders) {
                    if (item != cuurentColaider) {

                        Jump();
                        break;
                    }

                }
            }
        }

        void Jump() {

            rigidbody2D.AddForce(new Vector2(-400, 400));
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

        void OnCollisionEnter2D(Collision2D col) {
            if (col.transform.tag == "floor") {
                cuurentColaider = col.collider;
            }
            // IsJumping = true;
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

        float g = Physics.gravity.y * -1f;
        float v0 = 15;
        double GetAngle(Vector3 pos) {
            float L = Vector3.Distance(transform.position, pos);
            float h = pos.y - transform.position.y;

            double result = 0f;
            double tmp1 = Math.Pow(L, 2) * Math.Pow(v0, 4) - g * Math.Pow(L, 2) * (2 * h * Math.Pow(v0, 2) + g * Math.Pow(L, 2));
            if (tmp1 < 0)
                return -1;
            double tmp2 = L * Math.Pow(v0, 2);
            double tmp3 = tmp2 - Math.Sqrt(tmp1);
            double tmp4 = tmp3 / (g * Math.Pow(L, 2));
            result = Math.Atan(tmp4);
            return result;
        }
    }
}
