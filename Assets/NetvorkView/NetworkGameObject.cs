using UnityEngine;
using System.Collections;

public class NetworkGameObject : MonoBehaviour
{
    public GameObject gameObject;//= new GameObject();
    public Vector3 position = Vector3.zero;
    public Quaternion rotation = Quaternion.identity;
    public int ID;
    public bool IsMine = false;
    public MoveLerp moveLerp;


}
