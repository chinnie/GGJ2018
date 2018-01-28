using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushyBehavior : MonoBehaviour, IRobot
{

    [SerializeField] Vector3 _startposition;
    [SerializeField] Vector3 _endposition;
    [SerializeField] Vector3 _goalposition;
    [SerializeField] bool _UseAltBehavior = false;
    [SerializeField] private float speed = 1.0f;

    private float _timeStartedLerping;
    private float timeTakenDuringLerp = 1.0f;

    public Vector3 StartPosition
    {
        get
        {
            return _startposition;
        }

        set
        {
            _startposition = value;
        }
    }

    public Vector3 EndPosition
    {
        get
        {
            return _endposition;
        }

        set
        {
            _endposition = value;
        }
    }

    public bool AltBehavior
    {
        get;
        set;
    }

    public bool IsInteracting
    {
        get;
        set;
    }

    public bool IsActive
    {
        get;
        set;
    }

    // Use this for initialization
    void Start()
    {
        _startposition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_UseAltBehavior)
        {
            //Do a dance
            _goalposition = new Vector3(transform.position.x, _startposition.y + 2, transform.position.z);
        }
        else
        {
            _endposition = _goalposition;
        }

        if (IsActive)
        {
            float timeSinceStarted = Time.time - _timeStartedLerping;
            float percentageComplete = timeSinceStarted / timeTakenDuringLerp;

            transform.position = Vector3.Lerp(_startposition, _goalposition, percentageComplete);

            if (percentageComplete >= 1.0f)
            {
                IsActive = false;
            }
        }
    }

    public void TriggerAction()
    {
        if (IsActive)
        {
            Debug.Log("Busy");
            return;
        }
        IsActive = true;
        _startposition = transform.position;
        _timeStartedLerping = Time.time;
        _goalposition = CalculateGoal();
        Debug.Log("Pushy Go!");
    }

    public void Toggle()
    {
        Debug.Log("Pushy Toggle!");
        _UseAltBehavior = !_UseAltBehavior;
    }

    private Vector3 CalculateGoal()
    {
        RaycastHit hit;
        RaycastHit hit2;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        float distance = 0.0f;
        float distance2 = 0.0f;


        if (Physics.Raycast(transform.position, fwd, out hit, 10))
        {
            Debug.Log(hit.collider.gameObject.transform.position);

            distance = Vector3.Distance(hit.transform.position, this.StartPosition);
            distance = Mathf.FloorToInt(distance) - 1;

            // Do we push something?
            // Calculate new goal based off pushability of target.

            if (Physics.Raycast(hit.transform.position, fwd, out hit2, 10))
            {
                Debug.Log(hit.collider.gameObject.transform.position);

                distance2 = Vector3.Distance(hit2.transform.position, hit.transform.position);
                distance2 = Mathf.FloorToInt(distance2) - 1;
            }
        }
        else
        {
            Debug.Log("Nothing to hit!");
            distance = 10.0f;
        }
    
        timeTakenDuringLerp = (distance+distance2)/speed;
        return this.transform.position + transform.forward * (distance+distance2);
    }

    public void Spin(bool AlternateBehavior)
    {
       
    }
}
