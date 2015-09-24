using UnityEngine;
using System.Collections;

public class PrefabArray : MonoBehaviour {
     PrefabArray prefabArray;
    public GameObject[] objects;

    public PrefabArray() {
        if(prefabArray == null) {
            prefabArray = new PrefabArray();
            }
        }


    public PrefabArray Instance() {
        return prefabArray;
        }
    }
