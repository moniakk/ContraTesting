using UnityEngine;
using System.Collections;
namespace Turret {

    public class bullet1 : MonoBehaviour {
        Rigidbody2D rigidbody2D;

        void Start() {
          //  rigidbody2D = GetComponent<Rigidbody2D>();

           // Vector3 v3Force = 1000 * transform.up;
           // rigidbody2D.AddForce(v3Force);
        }


        void Update() {

           transform.Translate(0, 0.1f, 0);
        }
        void OnTriggerEnter2D(Collider2D collaider) {
            if (collaider.tag == "Player") {
                Destroy(collaider.gameObject);
                Destroy(gameObject);

            }
        }



        void OnCollisionEnter2D(Collision2D collision) {
            Destroy(this.gameObject, 0.3f);
        }


    }
}

