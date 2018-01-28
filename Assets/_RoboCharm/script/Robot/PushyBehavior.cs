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

    private bool IsSpinning = false;
    private float _spintimeStartedLerping;
    private float spintimeTakenDuringLerp = 1.0f;

    Quaternion fromAngle;
    Quaternion toAngle;

    [SerializeField] private AudioSource pushySound;
    [SerializeField] private AudioSource pushyRotate;

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
            //_goalposition = new Vector3(transform.position.x, _startposition.y + 2, transform.position.z);
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
                pushySound.Stop();
                IsActive = false;
                var bots = GameObject.FindGameObjectsWithTag("Bot");

                foreach (var bot in bots)
                {
                    if (bot.GetComponent<IRobot>() != null)
                    {
                        bot.GetComponent<IRobot>().ReSnapToGrid();
                    }
                    else
                    {
                        Debug.Log("Bots need tag AND IRobot!");
                    }
                }

                var props = GameObject.FindGameObjectsWithTag("Pushable");

                foreach (var prop in props)
                {
                    if (prop.GetComponent<IInteractable>() != null)
                    {
                        prop.GetComponent<IInteractable>().ReSnapToGrid();
                    }
                    else
                    {
                        Debug.Log("Props need tag AND IInteractable!");
                    }
                }
            }
        }

        if (IsSpinning)
        {
            float spintimeSinceStarted = Time.time - _spintimeStartedLerping;
            float spinpercentageComplete = spintimeSinceStarted / spintimeTakenDuringLerp;

            transform.rotation = Quaternion.Lerp(fromAngle, toAngle, spinpercentageComplete);

            if (spinpercentageComplete >= 1.0f)
            {
                IsSpinning = false;
                pushyRotate.Stop();
            }
        }
    }

    public void TriggerAction()
    {
        if (IsActive || IsSpinning)
        {
            Debug.Log("Busy");
            return;
        }
        IsActive = true;
        _startposition = transform.position;
        _timeStartedLerping = Time.time;
        _goalposition = CalculateGoal();
        Debug.Log("Pushy Go!");
        pushySound.Play();
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
            distance = Mathf.Clamp(Mathf.FloorToInt(distance) - 1, 0.0f, 10.0f);

            // Do we push something?
            // Calculate new goal based off pushability of target.

            if (Physics.Raycast(hit.transform.position, fwd, out hit2, 10))
            {
                if (hit.collider.gameObject.tag == "Bot" || hit.collider.gameObject.tag == "Pushable")
                {
                    Debug.Log(hit.collider.gameObject.transform.position);

                    distance2 = Vector3.Distance(hit2.transform.position, hit.transform.position);
                    distance2 = Mathf.Clamp(Mathf.FloorToInt(distance2) - 1, 0.0f, 10.0f);
                }
            }
        }
        else
        {
            Debug.Log("Nothing to hit!");
            distance = 10.0f;
        }

        Debug.Log("Distance :" + distance + "+" + distance2);
        timeTakenDuringLerp = (distance + distance2) / speed;
        return this.transform.position + transform.forward * (distance + distance2);
    }

    public void Spin(bool AlternateBehavior)
    {
        int angle = 90;
        _spintimeStartedLerping = Time.time;

        if (AlternateBehavior)
        {
            angle = -90;
        }
        Debug.Log("Pushy Spin!");
        if (!IsSpinning && !IsActive)
        {
            fromAngle = transform.rotation;

            toAngle = Quaternion.Euler(transform.eulerAngles + Vector3.up * angle);

            IsSpinning = true;
            pushyRotate.Play();
        }
    }

    public void ReSnapToGrid()
    {
        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y),
            Mathf.RoundToInt(transform.position.z));
    }
}
