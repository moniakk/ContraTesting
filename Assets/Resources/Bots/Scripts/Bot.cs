using UnityEngine;
using System.Collections;
namespace Bot {
    public class Bot : MonoBehaviour {

        public float DPS = 1f;
        public float movementSpeed;
        public Intelect intelect;
        protected bool IsStarted = false;
        [Range(3,20)]
        public float JumpDistance = 10f;
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
    }

    public enum Intelect {
        noob = 0,
        normal = 1,
        Profi = 2
    }
}