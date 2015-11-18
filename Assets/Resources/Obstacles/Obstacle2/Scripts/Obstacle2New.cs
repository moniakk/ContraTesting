using UnityEngine;
using System.Collections;

public class Obstacle2New : MonoBehaviour {
    public float Maxsize = 1f;
    [Range(0f, 3f)]
    public float Minsize = 0.1f;
    public float speed = 0.1f;
    public float start = 0f;
    public float sleep = 1f;
    float currentSize = 0;
    float currentsleep = 0;
    bool down = true;
    Vector3 currnetPosition;
    Renderer render;
    void Start() {

        currentsleep = sleep;
        currnetPosition = transform.position;
        render = GetComponent<Renderer>();
    }


    void Update() {


        currentsleep += Time.deltaTime;
        if (start > 0) {
            start -= Time.deltaTime;
            return;
        }
        if (sleep > currentsleep) {
            return;
        }


        if (down) {
            currentSize += speed;
        } else {
            currentSize -= speed;
        }

        if (currentSize <= Minsize && !down) {
            down = true;
            currentsleep = 0f;
            return;
        }

        if (currentSize > Maxsize && down) {
            down = false;
            return;
        }
        Vector3 v2 = new Vector3(.3f, currentSize, 1);

        transform.localScale = v2;

        float y = (Vector3.up * currentSize / 2).y + currnetPosition.y - transform.position.y;
        var tmp = (Vector3.up * currentSize / 2) + currnetPosition - transform.position;



        transform.Translate(tmp, Space.Self);

    }

    void MaterialCorrect() {
        if (render.material != null) {// render.material.mainTextureScale = new Vector2(transform.localScale.x , transform.localScale.y);
            render.material.mainTextureScale = new Vector2(transform.localScale.x * 5f, transform.localScale.y * 4f);
        }
    }
    void OnDrawGizmos() {
        MaterialCorrect();

    }
}
