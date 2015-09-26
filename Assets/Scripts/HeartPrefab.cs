using UnityEngine;
using System.Collections;

public class HeartPrefab : MonoBehaviour {

    DetailView detal;
    void Start() {
        detal = GameObject.Find("Canvas").GetComponent<DetailView>();
    }

    // Update is called once per frame
    void Update() {

    }
    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            detal.HeartEdit(1);
            Destroy(transform.gameObject);
        }

    }
}
