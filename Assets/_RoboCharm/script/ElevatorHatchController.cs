using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _RoboCharm.scripts
{
    public class ElevatorHatchController : MonoBehaviour
    {

        public bool isOpened = false;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void open()
        {
            GetComponent<Animator>().SetTrigger("isOpening");
            GetComponent<AudioSource>().Play();
            this.isOpened = true;
        }

    }
}
