using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoppyBehavior : MonoBehaviour, IRobot
{
    [SerializeField] Vector3 _startposition;
    [SerializeField] Vector3 _endposition;
    [SerializeField] bool _UseAltBehavior = false;

    private bool IsSpinning = false;
    private float _spintimeStartedLerping;
    private float spintimeTakenDuringLerp = 1.0f;

    Quaternion fromAngle;
    Quaternion toAngle;

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

    //public Vector3 EndPosition { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
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

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSpinning)
        {
            float spintimeSinceStarted = Time.time - _spintimeStartedLerping;
            float spinpercentageComplete = spintimeSinceStarted / spintimeTakenDuringLerp;

            transform.rotation = Quaternion.Lerp(fromAngle, toAngle, spinpercentageComplete);

            if (spinpercentageComplete >= 1.0f)
            {
                IsSpinning = false;
            }
        }
    }

    public void TriggerAction()
    {
        GetComponent<RobotHoppy>().Activate();
        Debug.Log("Hoppy Go!");
    }

    public void Toggle()
    {
        Debug.Log("Hoppy Toggle!");
        _UseAltBehavior = !_UseAltBehavior;
    }

    public void Spin(bool AlternateBehavior)
    {
        int angle = 90;
        _spintimeStartedLerping = Time.time;

        if (AlternateBehavior)
        {
            angle = -90;
        }
        Debug.Log("Hoppy Spin!");
        if (!IsSpinning)
        {
            fromAngle = transform.rotation;

            toAngle = Quaternion.Euler(transform.eulerAngles + Vector3.up * angle);

            IsSpinning = true;
        }
    }

    public void ReSnapToGrid()
    {
        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y),
            Mathf.RoundToInt(transform.position.z));
    }
}
