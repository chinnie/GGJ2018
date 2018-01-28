using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnyBehavior : MonoBehaviour, IRobot
{
    [SerializeField] Vector3 _startposition;
    [SerializeField] Vector3 _endposition;
    [SerializeField] bool _UseAltBehavior = false;

    private bool IsSpinning = false;
    private float _spintimeStartedLerping;
    private float spintimeTakenDuringLerp = 1.0f;

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
            
            if (spinpercentageComplete >= 1.0f)
            {
                IsSpinning = false;
            }
        }
    }

    public void TriggerAction()
    {
        if (!IsSpinning)
        {
            IsSpinning = true;
            _spintimeStartedLerping = Time.time;
            Debug.Log("Spinny Go!");
            // Get list of bots in scene

            var bots = GameObject.FindGameObjectsWithTag("Bot");

            // Call toggle on all bots but this one
            foreach (var bot in bots)
            {
                if (bot.GetComponent<IRobot>() != null)
                {
                    bot.GetComponent<IRobot>().Spin(_UseAltBehavior);
                }
                else
                {
                    Debug.Log("Bots need tag AND IRobot!");
                }
            }
        }
        else
        {
            Debug.Log("Already Spinning.");
        }
    }

    public void Toggle()
    {
        // Hacky has no toggle state!
        Debug.Log("Spinny Toggle");
        _UseAltBehavior = !_UseAltBehavior;
    }

    public void Spin(bool AlternateBehavior)
    {
        // Spinny does not spin!
    }

    public void ReSnapToGrid()
    {
        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y),
            Mathf.RoundToInt(transform.position.z));
    }
}
