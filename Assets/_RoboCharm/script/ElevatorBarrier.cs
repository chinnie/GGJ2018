﻿using NewtonVR;
using UnityEngine;

namespace _RoboCharm.scripts {
    public class ElevatorBarrier : MonoBehaviour {

        public GameObject PlayerHead;
        public bool KillPlayer = true;
        public Elevator ElevatorObj;

        // Use this for initialization
        private void Start () {
            Debug.Log("Barrier Start");
        }

        // Update is called once per frame
        private void Update () {}

        public void OnTriggerEnter (Collider other) {
            Debug.Log("Trigger Enter");
            Debug.Log(other.gameObject);
            Debug.Log(PlayerHead);
            if (other.gameObject != PlayerHead) {
                return;
            }
            Debug.Log("Hit Head");

            if (KillPlayer) {
                ElevatorObj.KillPlayer();
            }
            else {
                ElevatorObj.PassBarrier();
            }
        }
    }
}