using UnityEngine;
using System.Collections;

namespace Flying.FlyingPlane.Bullets.Bullet1 {
    public class Bomb1 : MonoBehaviour {

        Animator anim;
        void Start() {
            anim = GetComponent<Animator>();
        }

        void Destroys() {
            Destroy(gameObject);
        }

        void OnCollisionEnter2D(Collision2D collision) {
            anim.SetBool("explotion", true);
            GetComponent<Rigidbody2D>().isKinematic = true;
            Destroy(GetComponent<BoxCollider2D>());

        }



    }
}