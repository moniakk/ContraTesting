using UnityEngine;
using System.Collections;

public class BotBulletScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * 0.1f);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag != "Player" && col.transform.tag != "bullet")
        {
            Destroy(gameObject);
        }

    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
