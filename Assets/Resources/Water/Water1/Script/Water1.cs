using UnityEngine;
using System.Collections;

public class Water1 : MonoBehaviour {

    public float Position, Velocity;
    void Start () {
	
	}
	
	
	void Update () {
        float x = Height - TargetHeight;
        float acceleration = -k * x;

        Position += Velocity;
        Velocity += acceleration;
    }
}
