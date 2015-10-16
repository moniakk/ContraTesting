using UnityEngine;
using System.Collections;

public class MoveLerp : MonoBehaviour {

    public Vector3 position = Vector3.zero;
    public Quaternion rotation = Quaternion.identity;

    public float offset = 0;
    public bool IsMine = true;

    void Update() {
        if (!IsMine) {
            offset += Time.deltaTime;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, position, offset);
        }
    }
}
