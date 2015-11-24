using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Bot
{
    public class Bot2 : Bot
    {
        Collider2D currentFloor;
        public float ForceUp = 400;
        public float ForceLeft = 400;

        public LayerMask layerMask;

        Rigidbody2D currentRigidbody2D;
        bool finishPoint;
        bool IsJumping;

        void Start()
        {
            currentRigidbody2D = GetComponent<Rigidbody2D>();
        } 

        void Update()
        {
            if (IsStarted)
            {
                if (currentFloor != null)
                {
                    float x = transform.position.x + movementSpeed * Time.deltaTime;
                    finishPoint = x < currentFloor.bounds.min.x + 0.5f;
                    if (!finishPoint)
                    {
                        transform.Translate(Vector2.left * movementSpeed);
                    }
                    else { jump(); }

                }
            }


        }
        
        void OnTriggerStay2D(Collider2D col)
        {
            if (col.tag != "bot")
            {
                //return;
            }

            if (transform.position.x <= col.bounds.max.x + 2)
            {
                if (transform.position.y < col.bounds.max.y)
                {
                    if (transform.position.y > col.bounds.min.y)
                    {
                        float ColX = transform.GetComponent<BoxCollider2D>().bounds.max.x;
                        if (ColX >col.bounds.max.x + 2)
                        {
                            //jump();
                            if (IsJumping)
                            {
                                IsJumping = false;
                                currentRigidbody2D.AddForce((Vector2.left * ForceLeft) + Vector2.up * ForceUp);
                            }
                        }

                    }
                }

            }
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.tag == "floor")
            {
                currentFloor = col.collider;
            }
            IsJumping = true;
                    }
        
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Player")
            {
                IsStarted = true;
            }
                    }

        void jump()
        {
            if (IsJumping && CalcData(currentFloor))
            {
                IsJumping = false;
                currentRigidbody2D.AddForce((Vector2.left * ForceLeft) + Vector2.up * ForceUp);
            }
                    }
        
        [Range(0, 200)]
        public float BulletSpeed = 1f;//mps

        [Range(0.01f, 0.1f)]
        public float Step = 0.5f;
        public float MinY = -2;
        
        bool CalcData(Collider2D col)
        {

            Collider2D d = GetComponent<BoxCollider2D>();
            Vector2 originPosition = new Vector3(d.bounds.min.x, d.bounds.min.y);// transform.position;
            Vector2 speed = new Vector2(-ForceLeft / 200, ForceUp / 200) * BulletSpeed;
            Vector2 prevPosition = originPosition;

            List<RaycastHit2D> hit2D = new List<RaycastHit2D>();

            int itCount = 0;
            while (prevPosition.y > MinY)
            {
                itCount += 1;
                float stepTime = (float)itCount * Step;
                Vector2 currentPosition = originPosition + speed * stepTime + Physics2D.gravity * stepTime * stepTime / 2f;
                hit2D = Physics2D.RaycastAll(prevPosition, currentPosition - prevPosition, Mathf.Abs((prevPosition - currentPosition).magnitude), layerMask).ToList();
                hit2D = hit2D.Where(x => x.collider.tag == "floor").ToList();
                Debug.DrawLine(prevPosition, currentPosition, Color.green, 2, false);
                foreach (var item in hit2D)
                {
                    return item.collider != null && item.collider != col;
                }
                prevPosition = currentPosition;
            }

            return false;
        }

    }
}