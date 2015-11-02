using UnityEngine;
using System.Collections;

public class Obstacle1 : MonoBehaviour
{
    public float Maxsize = 1f;
    public float speed = 0.1f;
    public float start = 0f;
    public float sleep = 1f;
    float currentSize = 0;
    float currentsleep = 0;
    bool down=true;
    public GameObject obj;
    void Start()
    {
        currentsleep = sleep;
    }

    // Update is called once per frame
    void Update()
    {
        currentsleep += Time.deltaTime;
        if (start > 0)
        {
            start -= Time.deltaTime;
            return;
        }
        if (sleep> currentsleep)
        {
            return;
        }
        if (down)
        {
            currentSize += speed;
        }
        else { currentSize -= speed; }

        Vector2 v2 = new Vector2(1, currentSize);
        obj.transform.localScale = v2;


        if (currentSize > Maxsize)
        {
            down = false;
           
        }
        if (currentSize <= 0)
        {
            down = true; currentsleep = 0f;
        }
    }
}
