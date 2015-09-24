using UnityEngine;
using System.Collections;

public class CharacterControlShoot : MonoBehaviour {
    public GameObject[] guns;
    // Use this for initialization
    void Start() {

        }

    // Update is called once per frame

    void Update() {

        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            deleteGun();
            Transform dd = (Transform)Instantiate(guns[1].transform, transform.position, Quaternion.identity);
            dd.parent = transform;

            } else if(Input.GetKeyDown(KeyCode.Alpha2)) {
            deleteGun();
            Transform dd = (Transform)Instantiate(guns[0].transform, transform.position, Quaternion.identity);
            dd.parent = transform;
            }
        }

    void deleteGun() {
        var allChildren = gameObject.GetComponentsInChildren(typeof(Transform));
        for(int i = 0; i < allChildren.Length; i++) {
            if(allChildren[i].tag == "gun") {
                Destroy(allChildren[i].gameObject);
                }

            }
        }

    }
