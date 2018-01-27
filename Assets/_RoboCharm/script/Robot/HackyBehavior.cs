using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackyBehavior : MonoBehaviour, IRobot
{
    Vector3 _startposition;
    Vector3 _endposition;
    bool _UseAltBehavior = false;

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
    }
}
