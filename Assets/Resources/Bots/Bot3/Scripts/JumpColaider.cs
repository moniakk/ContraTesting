using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JumpColaider : MonoBehaviour {

    EdgeCollider2D ThisColaider;
    public List<Collider2D> colaiders = new List<Collider2D>();
    void Start() {
        ThisColaider = GetComponent<EdgeCollider2D>();
    }


    void OnTriggerEnter2D(Collider2D col) {

        if (col.tag == "floor" && !colaiders.Contains(col)) {
            colaiders.Add(col);
        }

    }

    void OnTriggerExit2D(Collider2D col) {
        colaiders.Remove(col);
    }

}
