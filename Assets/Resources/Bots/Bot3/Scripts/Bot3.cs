using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
namespace Bot {
    public class Bot3 : Bot {


        public bool AutoTargetPlayer;
        public bool Loop;
        public Vector3 EndPosition;
        Vector3 startPosition;
        Vector3 TargetPosition;
        Transform targetPlayer;
        Rigidbody2D rigidbody2D;
        bool IsJumping;
        bool IsStarted;
        bool barrier = false;
        bool LeftToRight;

        List<Collider2D> colaiders = new List<Collider2D>();
        Collider2D curentColaider;


        List<Collider2D> jumpColaider;
        BoxCollider2D BotCollaider;
        void Start() {
            jumpColaider = transform.FindChild("JumpColaider").GetComponent<JumpColaider>().colaiders;
            BotCollaider = GetComponent<BoxCollider2D>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            startPosition = transform.position;
            EndPosition = startPosition + EndPosition;
            if (!AutoTargetPlayer)
                TargetPosition = EndPosition;


        }


        void Update() {
            if (!IsStarted) return;
            if (AutoTargetPlayer && targetPlayer != null) {
                TargetPosition = targetPlayer.transform.position;
                MoveToTarget();
            }
            if (!AutoTargetPlayer) {
                MoveToTarget();

            }
            if (Input.GetMouseButtonDown(1)) {
                foreach (var item in colaiders) {
                    if (item != curentColaider) {

                        jump1(new Vector3(item.bounds.min.x, item.bounds.max.y, 0));
                        break;
                    }

                }
            }
        }

        private void jump1(Vector3 pos) {
            float result = (float)GetAngle(pos);
            float x = Convert.ToSingle(Math.Cos(result));
            float y = Convert.ToSingle(Math.Sin(result));
            //rigidbody2D.AddForce(new Vector2(x, y) * 15, ForceMode2D.Impulse);
            rigidbody2D.AddForce(ToAngle(result) * 15, ForceMode2D.Impulse);

        }
        Vector2 ToAngle(double angle) {
            double tmp = angle / 90;
            var x = Convert.ToSingle(1 - tmp);
            var y = Convert.ToSingle(tmp);
            return new Vector2(x, y);
        }

        bool NextPoint(Vector3 ObjMove) {
            bool result = false; ;
            foreach (var item in jumpColaider.Where(x => betwenX(x, BotCollaider, ObjMove))) {
                if (item.bounds.max.y < BotCollaider.bounds.min.y) {
                    result = true;
                }
            }

            return result;
        }

        void Jump() {
            if (!IsJumping) {
                IsJumping = true;

                Vector2 JumVecor;
                if (LeftToRight) {
                    JumVecor = new Vector2(300, 300);
                } else { JumVecor = new Vector2(-400, 400); }
                rigidbody2D.AddForce(JumVecor);
            }
        }

        void jumps(Vector3 nextPosition) {
            if (nextPosition.y > 0) {
                foreach (var item in jumpColaider.Where(x => x != curentColaider && x.bounds.max.y > BotCollaider.bounds.min.y)) {
                    Jump();
                    return;
                }
            }
        }

        void MoveToTarget() {
            Vector3 nextPosition = TargetPosition - transform.position;
            jumps(nextPosition);
            if (floorFinish() && !NextPoint(nextPosition.normalized)) {


            } else {

                if (Convert.ToInt32(EndPosition.x - transform.position.x) == 0) {
                    TargetPosition = startPosition;
                } else if (Convert.ToInt32(startPosition.x - transform.position.x) == 0) {
                    TargetPosition = EndPosition;
                }
                Move(nextPosition.normalized);
            }


        }

        void Move(Vector3 translation) {
            if (!IsJumping) {
                if (translation.x > 0) {
                    LeftToRight = true;
                    transform.rotation = new Quaternion(0, 180, 0, 1);
                } else {
                    transform.rotation = new Quaternion(0, 0, 0, 1);
                    LeftToRight = false;
                }
                Vector3 newPosition = transform.position + (translation * movementSpeed) * Time.deltaTime;
                transform.position = new Vector3(newPosition.x, transform.position.y, 0);
            }

        }

        void OnCollisionEnter2D(Collision2D col) {
            if (col.transform.tag == "floor") {
                curentColaider = col.collider;
                IsStarted = true;
            }
            IsJumping = false;

        }

        void OnTriggerEnter2D(Collider2D col) {
            if (col.tag == "Player") {
                IsStarted = true;
                targetPlayer = col.transform;

            }
            if (col.tag == "floor") {
                colaiders.Add(col);
            }

        }

        void OnTriggerLeave2D(Collider2D col) {
            colaiders.Remove(col);
        }

        bool floorFinish() {
            bool result = false;
            if (LeftToRight && curentColaider.bounds.max.x < BotCollaider.bounds.max.x) {
                result = true;
            } else if (!LeftToRight && curentColaider.bounds.min.x > BotCollaider.bounds.min.x) {
                result = true;
            }
            return result;
        }

        bool betwen(Collider2D Obj1, Collider2D Obj2, Vector2 Obj2move) {


            bool result = false;
            result = betwenX(Obj1, Obj2, Obj2move) && betwenY(Obj1, Obj2, Obj2move);

            return result;
        }
        bool betwenX(Collider2D Obj1, Collider2D Obj2, Vector2 Obj2move) {

            bool resultX = false;

            if ((Obj1.bounds.max.x >= Obj2.bounds.min.x + Obj2move.x && Obj1.bounds.min.x <= Obj2.bounds.min.x + Obj2move.x) || (Obj2.bounds.max.x + Obj2move.x >= Obj1.bounds.min.x && Obj2.bounds.min.x + Obj2move.x <= Obj1.bounds.min.x)) {
                resultX = true;
            }

            return resultX;
        }
        bool betwenY(Collider2D Obj1, Collider2D Obj2, Vector2 Obj2move) {


            bool resultY = false;

            if ((Obj1.bounds.max.y >= Obj2.bounds.min.y + Obj2move.y && Obj1.bounds.min.y <= Obj2.bounds.min.y + Obj2move.y) || (Obj2.bounds.max.y + Obj2move.y >= Obj1.bounds.min.y && Obj2.bounds.min.y + Obj2move.y <= Obj1.bounds.min.y)) {
                resultY = true;
            }
            return resultY;
        }

        float g = Physics.gravity.y;
        float v0 = 15;
        double GetAngle(Vector3 pos) {

            //float L = pos.x - transform.position.x;
            //float h = pos.y - transform.position.y;
            float L = pos.x - BotCollaider.bounds.min.x;
            float h = pos.y - BotCollaider.bounds.min.y;
            double result = 0f;
            double tmp1 = Math.Pow(L, 2) * Math.Pow(v0, 4) - g * Math.Pow(L, 2) * (2 * h * Math.Pow(v0, 2) + g * Math.Pow(L, 2));
            if (tmp1 < 0)
                return -1;
            double tmp2 = L * Math.Pow(v0, 2);
            double tmp3 = tmp2 - Math.Sqrt(tmp1);
            double tmp4 = tmp3 / (g * Math.Pow(L, 2));
            result = Math.Atan(tmp4) * 180 / Math.PI;
            //  var result2 = tmp4 * 180 / Math.PI;
            return result;
        }
    }
}
