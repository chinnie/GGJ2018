using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robot_boomy : MonoBehaviour {
    [SerializeField] private bool RobotUseful = true;
    [SerializeField] private bool isActive = false;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 goalPosition;
    private Vector3 endPosition;
    private Vector3 startPosition;

    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        if (!RobotUseful)
        {
            //plays animation in place

            // dance

        }
        else
        {
            endPosition = goalPosition;
        }
        if (isActive)
        {

            //explode

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
        // reset EndPostion
        endPosition = goalPosition;
    }
}
