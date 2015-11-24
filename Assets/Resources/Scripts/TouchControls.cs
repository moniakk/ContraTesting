using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;
public class TouchControls : MonoBehaviour {

    PlatformerCharacter2D thePlayer;
    float moveSpeed;
    bool crouch;
    bool jump;
    void Start() {
        thePlayer = FindObjectOfType<PlatformerCharacter2D>();
    }


    void Update() {
        if (Mathf.Abs(moveSpeed) < 1f && Mathf.Abs(moveSpeed) > 0f) {
            if (moveSpeed > 0) {
                moveSpeed += 0.1f;
            } else {
                moveSpeed -= 0.1f;
            }

        }
        Debug.Log("move= " + moveSpeed + " crouch = " + crouch);
        thePlayer.Move(moveSpeed, crouch, false);
    }




    public void LeftArrow() {

        moveSpeed = -0.2f;
    }

    public void RightArrow() {
        moveSpeed = 0.2f;
    }

    public void UpArrow() { thePlayer.Move(0, false, true); }

    public void DownArrow() {
        crouch = true;
    }

    public void UnpressedArrow() {
        moveSpeed = 0;
        crouch = false;
    }
}
