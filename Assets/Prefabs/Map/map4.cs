using UnityEngine;
using System.Collections;

public class map4 : MonoBehaviour
{
    public float distance = 1;
    public float speed = 0.01f;


    public float current;
    bool leftToRight;
    Vector2 currPos;
    Rigidbody2D rigidbody2D;
    void Start()
    {
        
        rigidbody2D = GetComponent<Rigidbody2D>();
        currPos = transform.position;
    }


    void Update()
    {
        if (current < -distance)
        {
            leftToRight = true;
            //  current = -distance;
        }
        if (current > distance)
        {
            leftToRight = false;
            current = distance;
        }


        if (leftToRight)
        {
            current += speed;
        }
        else
        {
            current -= speed;

        }

        rigidbody2D.MovePosition(currPos + new Vector2(current, 0));

    }
}
