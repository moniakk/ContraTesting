using UnityEngine;
using System.Collections;

public class WaterDetector : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D Hit) {
        if (Hit.tag == "Fish") {
            transform.parent.GetComponent<Water3>().Splash(transform.position.x, .1f);
        }
        if (Hit.transform.GetComponent<Rigidbody2D>() != null) {
            transform.parent.GetComponent<Water3>().Splash(transform.position.x, Hit.transform.GetComponent<Rigidbody2D>().velocity.y * Hit.transform.GetComponent<Rigidbody2D>().mass / 40f);
        }
    }

    /*void OnTriggerStay2D(Collider2D Hit)
    {
        //print(Hit.name);
        if (Hit.rigidbody2D != null)
        {
            int points = Mathf.RoundToInt(Hit.transform.localScale.x * 15f);
            for (int i = 0; i < points; i++)
            {
                transform.parent.GetComponent<Water>().Splish(Hit.transform.position.x - Hit.transform.localScale.x + i * 2 * Hit.transform.localScale.x / points, Hit.rigidbody2D.mass * Hit.rigidbody2D.velocity.x / 10f / points * 2f);
            }
        }
    }*/

}
