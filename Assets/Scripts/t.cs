using UnityEngine;
using System.Collections;

public class t : MonoBehaviour {

    void Start()
    {
     //   GetComponent<Rigidbody2D>().velocity = new Vector2(9.3f,9.3f);
    }

    public float xSpeed = 10;

    public Transform tar;

    void SetJumpSpeed()
    {
        float g = -Physics.gravity.y;

        float xTar = tar.position.x - transform.position.x;
        float yTar = -(tar.position.y - transform.position.y);

        float sX = xSpeed * (xTar > 0 ? 1 : -1);

        float t = xTar / sX;
        float y = t * t * g / 2 - yTar;

        if (y < 0 || Mathf.Abs(t - 0) < Mathf.Epsilon)
        {
        //    Debug.Log("Can't reach target for given x speed");
            return;
        }

        float s = y / t;

        GetComponent<Rigidbody2D>().velocity = new Vector3(sX, s, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetJumpSpeed();
        }
    }


}
