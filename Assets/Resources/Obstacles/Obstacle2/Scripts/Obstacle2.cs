using UnityEngine;
using System.Collections;

public class Obstacle2 : MonoBehaviour
{

    public float Maxsize = 1f;
    [Range(0.1f, 3f)]
    public float Minsize = 0.1f;
    public float speed = 0.1f;
    public float start = 0f;
    public float sleep = 1f;
    float currentSize = 0;
    float currentsleep = 0;
    bool down = true;
    Vector3 currnetPosition;
    void Start()
    {
        currentsleep = sleep;
        currnetPosition = transform.position;
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
        if (sleep > currentsleep)
        {
            return;
        }


        if (down)
        {
            currentSize += speed;
        }
        else
        {
            currentSize -= speed;
        }

        if (currentSize <= Minsize && !down)
        {
            down = true;
            currentsleep = 0f;
            return;
        }

        if (currentSize > Maxsize && down)
        {
            down = false; return;
        }

        Vector2 v2 = new Vector2(1, currentSize);
        transform.localScale = v2;

        // transform.position = new Vector2(currnetPosition.x, currnetPosition.y + );
        //var tmp = transform.position - currnetPosition + (Vector3.up * currentSize / 2);

        float y = (Vector3.up * currentSize / 2).y + currnetPosition.y - transform.position.y;
        var tmp = (Vector3.up * currentSize / 2) + currnetPosition - transform.position;

        //transform.Translate( currnetPosition.x, (currentSize / 2), currnetPosition.y,Space.Self);

        transform.Translate(tmp,Space.Self);
    }
}
