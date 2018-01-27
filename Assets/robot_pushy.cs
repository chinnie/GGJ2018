using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robot_pushy : MonoBehaviour {

    private bool RobotUseful = false;
    private bool isActive = true;
    [SerializeField] private int speed;
    [SerializeField] private Vector3 endPosition;
    private Vector3 startPosition;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        if (isActive & RobotUseful) {

            //move forward

            transform.position = Vector3.Lerp(startPosition, endPosition, speed * Time.deltaTime);

        } else if (isActive & !RobotUseful)
        {
            //plays animation in place
        }
		
	}

    //When activated perform this action
    void Activate()
    {
        //Push will move forward in a straigt line 
        isActive = !isActive;
    }

    //Toggle useful and silly functions
    void Toggle()
    {
        RobotUseful = !RobotUseful;
    }
}
