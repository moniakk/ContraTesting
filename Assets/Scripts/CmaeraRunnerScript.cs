using UnityEngine;
using System.Collections;

public class CmaeraRunnerScript : MonoBehaviour {

    public Transform Player;
    // Use this for initialization



    void Update() {
        if(Player != null) {
             // transform.position = new Vector3(Player.position.x + 6, 0, -10);
            transform.position = new Vector3(Player.position.x + 6, Player.position.y-3, -10);
        }
        }
    }
