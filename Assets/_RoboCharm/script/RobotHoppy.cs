﻿using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.UX;
using UnityEngine;

public class RobotHoppy : MonoBehaviour
{

    [SerializeField]
    private bool RobotUseful = true;
    [SerializeField]
    private bool isActive = true;
    [SerializeField]
    private float desiredJumpTime = 1;
    [SerializeField]
    private float jumpDistance = 2;
    [SerializeField]
    private float maxHeightIncrease = 2;
    [SerializeField]
    private Vector3 jumpDirection = new Vector3(1, 0, 0);

    private readonly Vector3 gravity = new Vector3(0, -9.81f, 0);
    private Vector3 endPosition;
    private Vector3 startPosition;
    private float startTime;
    private Vector3 up;
    private Vector3 startVelocity;
    private float jumpTime;
    private Joint joint;
    private Rigidbody targetRigidbody;
    private bool wasActive;

    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
        up = -gravity.normalized;
        jumpDirection.Normalize();

        if (isActive) {
            CalculateJump();
        }

        joint = GetComponent<Joint>();
        wasActive = isActive;
    }

    // Update is called once per frame
    void Update() {
        if (isActive && !wasActive) {
            CalculateJump();
        }

        wasActive = isActive;

        if (!isActive) {
            return;
        }

        float timeElapsed = Time.time - startTime;
        if (timeElapsed > jumpTime) {
            transform.position = endPosition;
            isActive = false;
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            if (joint != null) {
                joint.connectedBody = targetRigidbody;
                rigidbody.isKinematic = false;

            }
        }
        else
        {
            //Debug.Log(startPosition);
            //Debug.Log(startVelocity);
            //Debug.Log(timeElapsed);
            //Debug.Log(gravity);
            transform.position = startPosition + startVelocity * timeElapsed + 0.5f * gravity * timeElapsed * timeElapsed;
        }


    }

    //When activated perform this action
    public void Activate()
    {
        //Push will move forward in a straigt line 
        isActive = !isActive;
        if (isActive) {
            CalculateJump();
        }
    }

    private void CalculateJump()
    {
        startPosition = transform.position;
        startTime = Time.time;

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;

        if (!RobotUseful)
        {
            //plays animation in place
            endPosition = startPosition;
            startVelocity = -0.5f * gravity * desiredJumpTime;
        }
        else
        {
            Vector3 castStart = transform.position + transform.forward * jumpDistance;
            Debug.Log("target in plane: " + castStart);
            castStart += up * maxHeightIncrease;
            Vector3 targetPoint = FindTargetBelowPoint(castStart);
            Debug.Log("targetPoint=" + targetPoint);
            if (float.IsNaN(targetPoint.sqrMagnitude))
            {
                endPosition = startPosition;
                startVelocity = -0.5f * gravity * desiredJumpTime;
            }
            else {
                endPosition = targetPoint;
                Debug.Log("endPosition=" + endPosition);
                //solution of kinematics equation for velocity
                jumpTime = desiredJumpTime;
                startVelocity = (endPosition - startPosition) * 1 / jumpTime - 0.5f * gravity * jumpTime;
            }
        }
    }

    //Toggle useful and silly functions
    void Toggle()
    {
        RobotUseful = !RobotUseful;
    }

    private Vector3 FindTargetBelowPoint (Vector3 point) {
        RaycastHit hitInfo;
        Collider collider = GetComponent<Collider>();
        if (Physics.Raycast(point, -up, out hitInfo)) {
            targetRigidbody = hitInfo.rigidbody;
            Vector3 colliderOffset = collider.bounds.center - transform.position;
            return hitInfo.point + hitInfo.normal * (collider.bounds.extents.y - colliderOffset.y);
        }
        return point + up * float.NegativeInfinity;
    }

    private void OnCollisionEnter (Collision other) {

        Debug.Log("Collision");
        //don't fall if at target
        if ((transform.position - endPosition).sqrMagnitude < float.Epsilon * float.Epsilon)
        {
            Debug.Log("Collided with target");
            return;
        }

        Vector3 groundPoint = FindTargetBelowPoint(transform.position);
        endPosition = groundPoint;
        Debug.Log(endPosition);

        startVelocity = Vector3.zero;
        startPosition = transform.position;
        startTime = Time.time;
        jumpTime = Mathf.Sqrt(2 * (endPosition - startPosition).magnitude / gravity.magnitude);
        
    }
}
