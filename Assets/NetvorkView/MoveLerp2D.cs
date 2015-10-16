using UnityEngine;
using System.Collections;

public class MoveLerp2D : MonoBehaviour {

    public Vector2 position = Vector2.zero;
    public Quaternion rotation = Quaternion.identity;

    public float offset = 0;
    public bool IsMine = false;

    void Update() {
        if (!IsMine) {
            offset += Time.deltaTime;
            gameObject.transform.position = Vector2.Lerp(gameObject.transform.position, position, offset *0.9f);
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, rotation, offset * 0.9f);
        }
    }
}
