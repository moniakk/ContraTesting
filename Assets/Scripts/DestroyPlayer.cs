using UnityEngine;
using System.Collections;

public class DestroyPlayer : MonoBehaviour {
    DetailView detal;

	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {

          //  detal.HeartEdit(-1);

          

           
        }

    }
}
