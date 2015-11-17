using UnityEngine;
using System.Collections;

public class NetworkGameObject : MonoBehaviour {
    public GameObject gameObject=null;//= new GameObject();
    public Vector3 position = Vector3.zero;
    public Quaternion rotation = Quaternion.identity;
    public int ID;
    public bool IsMine = false;
    public MoveLerp moveLerp;


}

public class NetworkGameObject2D : MonoBehaviour {
    public GameObject gameObject=null;//= new GameObject();
    public Vector2 position = Vector2.zero;
    public Quaternion rotation = Quaternion.identity;
    public int ID;
    public bool IsMine = false;
    public MoveLerp2D moveLerp;


}
