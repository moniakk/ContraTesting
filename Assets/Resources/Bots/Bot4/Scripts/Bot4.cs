using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Bot {
    public class Bot4 : Bot {
        List<WayPoint> collaiders = new List<WayPoint>();
        Rigidbody2D rigidbody;
        public Transform TargetPlayer = null;
        Collider2D curentColaider;
        bool IsJumping;
        bool LeftToRight;
        void Start() {
            rigidbody = GetComponent<Rigidbody2D>();
        }


        void Update() {
            if (IsStarted) {

            }
            if (Input.GetKeyDown(KeyCode.Space)) {
                Jump(TargetPlayer.position);
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

        void Jump(Vector3 position) {
            if (Vector3.Distance(transform.position, position) <= JumpDistance) {
                float offset = ( position- transform.position ).y+0.8f;
                var vector = findInitialVelocity(transform.position, position, offset);
                Vector2 force = new Vector2(vector.x, vector.y);
                Debug.Log(force.x + "  " + force.y);
                rigidbody.velocity = force;
                //  rigidbody.AddForce(force* 43.0f);
            }
        }


        void colaidersAdd(Collider2D collaider) {
            if (collaiders.Exists(x => x.Collider == collaider)) return;
            WayPoint wayPoint = new WayPoint();
            wayPoint.Collider = collaider;
            Vector2 cuurentPointMax = getMaxSurface(collaider);
            Vector2 cuurentPointMin = getMinSurface(collaider);
            wayPoint.PossiblePoints.Add(new PointType() { Point = cuurentPointMax });
            wayPoint.PossiblePoints.Add(new PointType() { Point = cuurentPointMin });
            collaiders.Add(wayPoint);

            foreach (var wayPoints in collaiders.Where(x => x != wayPoint).ToList()) {
                foreach (var point in wayPoints.PossiblePoints.Where(x => !x.jump)) {
                    if (Vector2.Distance(point.Point, cuurentPointMax) <= JumpDistance || Vector2.Distance(point.Point, cuurentPointMin) <= JumpDistance) {
                        wayPoint.PossiblePoints.Add(point);

                    }
                }
            }



            var dd = collaiders.Where(p => p.PossiblePoints.Exists(x => Vector2.Distance(x.Point, cuurentPointMax) <= JumpDistance || Vector2.Distance(x.Point, cuurentPointMin) <= JumpDistance)).ToList();



        }

        Vector2 getMaxSurface(Collider2D collaider) {
            return new Vector2(collaider.bounds.max.x, collaider.bounds.max.y);
        }
        Vector2 getMinSurface(Collider2D collaider) {
            return new Vector2(collaider.bounds.min.x, collaider.bounds.max.y);
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
                TargetPlayer = col.transform;
            }
            if (col.tag == "floor") {
                colaidersAdd(col);
                // collaiders.Add(col);
            }

        }

        void OnTriggerLeave2D(Collider2D col) {
            // collaiders.Remove(col);
        }
        private Vector3 findInitialVelocity(Vector3 startPosition, Vector3 finalPosition, float maxHeightOffset = 0.6f, float rangeOffset = 0.11f) {
            float correctY = startPosition.y;
            startPosition.y = startPosition.y - correctY;
            finalPosition.y = finalPosition.y - correctY;

            Vector3 newVel = new Vector3();
            Vector3 direction = new Vector3(finalPosition.x, 0f, finalPosition.z) - new Vector3(startPosition.x, 0f, startPosition.z);
            float range = direction.magnitude;
            range += rangeOffset;
            Vector3 unitDirection = direction.normalized;
            float maxYPos = startPosition.y + maxHeightOffset;

            if (range / 2f > maxYPos)
                maxYPos = range / 2f;

            newVel.y = Mathf.Sqrt(-2.0f * Physics.gravity.y * (maxYPos - startPosition.y));
            float timeToMax = Mathf.Sqrt(-2.0f * (maxYPos - startPosition.y) / Physics.gravity.y);
            float timeToTargetY = Mathf.Sqrt(-2.0f * (maxYPos - finalPosition.y) / Physics.gravity.y);
            float totalFlightTime = timeToMax + timeToTargetY;
            float horizontalVelocityMagnitude = range / totalFlightTime;
            newVel.x = horizontalVelocityMagnitude * unitDirection.x;
            newVel.z = horizontalVelocityMagnitude * unitDirection.z;
            return newVel;
        }
    }

    public class WayPoint {
        public Collider2D Collider { get; set; }
        public List<PointType> PossiblePoints = new List<PointType>();


    }
    public class PointType {
        public Vector2 Point { get; set; }
        public bool jump { get; set; }
    }
}