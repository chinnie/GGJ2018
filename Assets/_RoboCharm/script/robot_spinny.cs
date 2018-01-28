using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robot_spinny : MonoBehaviour {

    [SerializeField] private bool RobotUseful = true;
    [SerializeField] private bool isActive = true;
    [SerializeField] private float speed;

   

	// Use this for initialization
	void Start () {
      

    }
	
	// Update is called once per frame
	void Update () {

        if (isActive & !RobotUseful)
        {
            //spins in place
            transform.Rotate(Time.deltaTime * speed, Time.deltaTime * speed * 2, Time.deltaTime );


        } else if (isActive & RobotUseful) {

            //rotates all robots 90 degrees
            var bots = GameObject.FindGameObjectsWithTag("Bot");

            // Call toggle on all bots but this one
            foreach (var bot in bots)
            {
                if (bot.GetComponent<IRobot>() != null)
                {
                    Quaternion currentRoation = bot.gameObject.transform.rotation;
                    bot.transform.Rotate(new Vector3(0, 90, 0), Space.Self);
                }
                else
                {
                    Debug.Log("Bots need tag AND IRobot!");
                }
            }

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
        if (RobotUseful){
            transform.Rotate(0, 0, 0);
        }
        
    }
}
