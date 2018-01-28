using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackyBehavior : MonoBehaviour, IRobot
{
    [SerializeField] Vector3 _startposition;
    [SerializeField] Vector3 _endposition;
    [SerializeField] bool _UseAltBehavior = false;

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
    }

    public void TriggerAction()
    {
        Debug.Log("Hacky Go!");
        // Get list of bots in scene

        var bots = GameObject.FindGameObjectsWithTag("Bot");

        // Call toggle on all bots but this one
        foreach (var bot in bots)
        {
            if (bot.GetComponent<IRobot>() != null)
            {
                bot.GetComponent<IRobot>().Toggle();
            }
            else
            {
                Debug.Log("Bots need tag AND IRobot!");
            }
        }
    }

    public void Toggle()
    {
        // Hacky has no toggle state!
        //_UseAltBehavior = !_UseAltBehavior;
    }
}
