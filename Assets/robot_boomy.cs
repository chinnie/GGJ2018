using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robot_boomy : MonoBehaviour {
    [SerializeField] private bool RobotUseful = true;
    [SerializeField] private bool isActive = false;
    [SerializeField] private GameObject explosion;
    [SerializeField] private AudioSource explosionSound;



    // Use this for initialization
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {

        if (!RobotUseful)
        {
            //plays animation in place

            // dance or wobble

        }
        
        if (isActive && RobotUseful)
        {

            //explode
            Instantiate(explosion, transform.position, transform.rotation);
            explosionSound.Play();
            Destroy(gameObject);
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
