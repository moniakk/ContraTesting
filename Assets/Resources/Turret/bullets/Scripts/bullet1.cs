using UnityEngine;
using System.Collections;
namespace Turret {

    public class bullet1 : MonoBehaviour {

     
        void Start() {

        }

       
        void Update() {
            transform.Translate(0, 0.03f, 0);
        }
    }
}

