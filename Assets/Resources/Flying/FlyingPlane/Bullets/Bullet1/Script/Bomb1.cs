using UnityEngine;
using System.Collections;

namespace Flying.FlyingPlane.Bullets.Bullet1 {
    public class Bomb1 : MonoBehaviour {

        Animator anim;
        public float boomSize = 2f;
        void Start() {
            anim = GetComponent<Animator>();
        }

        void Destroys() {
            Destroy(gameObject);
        }


        void AnimationIsStarted() {
            transform.localScale = transform.localScale * boomSize;
        }

        void OnCollisionEnter2D(Collision2D collision) {

            anim.SetBool("explotion", true);
            GetComponent<Rigidbody2D>().isKinematic = true;
            Destroy(GetComponent<BoxCollider2D>());


        }

        //void OnTriggerEnter2D(Collider2D col) {
        //    if (col.tag!=this.tag) {
        //        anim.SetBool("explotion", true);
        //        GetComponent<Rigidbody2D>().isKinematic = true;
        //        Destroy(GetComponent<BoxCollider2D>());
        //    }
          
        //}

    }
}