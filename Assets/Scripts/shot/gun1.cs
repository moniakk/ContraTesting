using UnityEngine;
using System.Collections;

public class gun1 : shoot {
    public GameObject bullet;



    public override void shootStart() {
       
        //var newBullet = Instantiate(bullet, transform.TransformPoint(-5, 0.52f, 0), Quaternion.identity);
        var newBullet = (GameObject)Instantiate(bullet, FirePoint.position, Quaternion.identity);
        Rigidbody2D currentRigiedBody = newBullet.GetComponent<Rigidbody2D>();

        currentRigiedBody.AddForce(Vector2.right * 90);
        Destroy(newBullet, 3);
        }

    }
