using UnityEngine;
using System.Collections;

public class gun2 : shoot {

    public GameObject bullet;



    public override void shootStart() {
        //var newBullet = Instantiate(bullet, transform.TransformPoint(-5, 0.52f, 0), Quaternion.identity);
        for(int i = 0; i < 3; i++) {
        var newBullet = (GameObject)Instantiate(bullet, transform.TransformPoint(0.7f, 0.16f, 0), Quaternion.identity);
            Rigidbody2D currentRigiedBody = newBullet.GetComponent<Rigidbody2D>();
            currentRigiedBody.AddForce(Vector2.right * 90 + Vector2.up * (i - 1.5f)*10);
            Destroy(newBullet, 3);

        
            }

        }

    }
