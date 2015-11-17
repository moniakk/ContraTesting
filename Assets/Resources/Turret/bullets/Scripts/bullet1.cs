using UnityEngine;
using System.Collections;
namespace Turret {

    public class bullet1 : MonoBehaviour {

        void Start() {
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

