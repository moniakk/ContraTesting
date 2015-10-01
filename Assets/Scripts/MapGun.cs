using UnityEngine;
using System.Collections;

public class MapGun : MonoBehaviour {
    bool drop;
    // Use this for initialization
    void Start() {

        }

    // Update is called once per frame
    void Update() {
        if(drop) {
            transform.Translate(Vector2.down * 0.1f);
          

            }
        }
    void OnTriggerEnter2D(Collider2D hit) {
        if(hit.tag == "bullet") {
            drop = true;
            } else if(hit.tag == "Player") {

            hit.SendMessage("SetGun", 1, SendMessageOptions.DontRequireReceiver);
            }

        }


    }
