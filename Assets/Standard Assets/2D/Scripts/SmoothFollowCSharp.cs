// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden

using UnityEngine;
using System.Collections;

// Place the script in the Camera-Control group in the component menu
[AddComponentMenu("Camera-Control/Smooth Follow CSharp")]

public class SmoothFollowCSharp : MonoBehaviour {

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    private Camera camera;

    void Start() {
    camera = Camera.main;
        }

    // Update is called once per frame
    void Update() {
        if(target) {
            Vector3 point = camera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.1f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            }

        }
    }