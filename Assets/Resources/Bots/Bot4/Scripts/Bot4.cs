using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Bot {
    public class Bot4 : Bot {
        List<WayPoint> collaiders = new List<WayPoint>();
        List<Collider2D> vv3 = new List<Collider2D>();
        Rigidbody2D rigidbody;
        public Transform TargetPlayer = null;
        Collider2D curentColaider;

        public GameObject platform;
        bool IsJumping;
        bool LeftToRight;
        void Start() {
            rigidbody = GetComponent<Rigidbody2D>();
            for (int i = 0; i < 50; i++) {
                var d = (GameObject)Instantiate(platform, new Vector3(Random.Range(1, 40), Random.Range(1, 15), 0), Quaternion.identity);
                d.GetComponent<map4>().speed = Random.Range(0.01f, 0.2f);
            }
        }


        void Update() {

            if (IsStarted) {

            }
            if (Input.GetKeyDown(KeyCode.Space)) {

                //Jump(TargetPlayer.position);
            }
            UpdateList();
            foreach (var item in collaiders) {
                foreach (var v3 in item.PossiblePoints) {
                    Debug.DrawLine(item.Point, v3.Point, v3.Jump ? Color.red : Color.green);

                }

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
                float offset = (position - transform.position).y + 0.8f;
                var vector = findInitialVelocity(transform.position, position, offset);
                Vector2 force = new Vector2(vector.x, vector.y);
                Debug.Log(force.x + "  " + force.y);
                rigidbody.velocity = force;
                //  rigidbody.AddForce(force* 43.0f);
            }
        }


        void colaidersAdd(Collider2D collaider) {
            if (collaiders.Exists(x => x.Collider == collaider)) return;
            vv3.Add(collaider);
            return;
            Vector2 cuurentPointMax = getMaxSurface(collaider);
            Vector2 cuurentPointMin = getMinSurface(collaider);
            WayPoint wayPoint = new WayPoint();
            wayPoint.Collider = collaider;
            wayPoint.Point = cuurentPointMin;
            wayPoint.PossiblePoints.Add(new ConnectedWayPoint() { Point = cuurentPointMax, Distance = Vector2.Distance(cuurentPointMax, cuurentPointMin) });
            collaiders.Add(wayPoint);
            SetReference(wayPoint);
            wayPoint = new WayPoint();
            wayPoint.Point = cuurentPointMax;
            wayPoint.Collider = collaider;
            wayPoint.PossiblePoints.Add(new ConnectedWayPoint() { Point = cuurentPointMin, Distance = Vector2.Distance(cuurentPointMax, cuurentPointMin) });
            collaiders.Add(wayPoint);

            SetReference(wayPoint);



        }
        int tmp;
        void UpdateList() {
            tmp = tmp+ 1;
            if (tmp < 0) {
               
                return;
            }
            tmp = 0;
            collaiders = new List<WayPoint>();
            foreach (var collaider in vv3) {
              
              
                Vector2 cuurentPointMax = getMaxSurface(collaider);
                Vector2 cuurentPointMin = getMinSurface(collaider);
                WayPoint wayPoint = new WayPoint();
                wayPoint.Collider = collaider;
                wayPoint.Point = cuurentPointMin;
                wayPoint.PossiblePoints.Add(new ConnectedWayPoint() { Point = cuurentPointMax, Distance = Vector2.Distance(cuurentPointMax, cuurentPointMin) });
                collaiders.Add(wayPoint);
                SetReference(wayPoint);
                wayPoint = new WayPoint();
                wayPoint.Point = cuurentPointMax;
                wayPoint.Collider = collaider;
                wayPoint.PossiblePoints.Add(new ConnectedWayPoint() { Point = cuurentPointMin, Distance = Vector2.Distance(cuurentPointMax, cuurentPointMin) });
                collaiders.Add(wayPoint);

                SetReference(wayPoint);

            }

        }

        void SetReference(WayPoint wayPoint) {
            foreach (var wayPoints in collaiders.Where(x => x != wayPoint).ToList()) {
                float distance = Vector2.Distance(wayPoint.Point, wayPoints.Point);
                if (distance <= JumpDistance) {
                    wayPoint.PossiblePoints.Add(new ConnectedWayPoint() { Point = wayPoints.Point, Jump = wayPoint.Collider != wayPoints.Collider, Distance = distance });
                    wayPoints.PossiblePoints.Add(new ConnectedWayPoint() { Point = wayPoint.Point, Jump = wayPoint.Collider != wayPoints.Collider, Distance = distance });
                }
            }
        }

        void pathFinder() {
            bool finishFind = false;
            Vector2 start = new Vector2();
            Vector2 end = new Vector2();
            WayPoint wayPoint = collaiders.First(x => x.Point == start);
            while (start != end && !finishFind) {
                foreach (var item in wayPoint.PossiblePoints) {
                    if (item.Point == end) {
                        finishFind = true;
                    }

                }
            }

        }

        //Vector2 GetPath(WayPoint wayPoint) {


        //}


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
        public Vector2 Point { get; set; }
        public List<ConnectedWayPoint> PossiblePoints = new List<ConnectedWayPoint>();


    }
    public class ConnectedWayPoint {
        public Vector2 Point { get; set; }
        public bool Jump;
        public float Distance;
    }
}