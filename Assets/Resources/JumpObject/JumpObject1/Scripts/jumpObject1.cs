using UnityEngine;
using System.Collections;

public class jumpObject1 : MonoBehaviour
{
    public float JumpForce = 1500f;
    float NextJump;


    void Start()
    {

    }


    void Update()
    {


    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if ((Time.fixedTime - NextJump) > 0.2f)
        {
            NextJump = Time.fixedTime;
            var rigidbody = col.gameObject.GetComponent<Rigidbody2D>();


            rigidbody.AddForce(transform.up * JumpForce);
        }


    }

    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    if ((Time.fixedTime - NextJump) > 0.2f)
    //    {
    //        NextJump = Time.fixedTime;
    //        var rigidbody = col.gameObject.GetComponent<Rigidbody2D>();


    //        rigidbody.AddForce(transform.up * JumpForce);
    //    }


    //}
}
