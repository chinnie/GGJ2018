using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _RoboCharm.scripts;

public class levelCompleted : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        GameObject elevator = GameObject.FindGameObjectWithTag("Elevator");
        elevator.GetComponent<Elevator>().DisableBarrier();
    }
}
