using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomyBehavior : MonoBehaviour, IRobot
{
    [SerializeField] Vector3 _startposition;
    [SerializeField] Vector3 _endposition;
    [SerializeField] bool _UseAltBehavior = false;

    [SerializeField] private GameObject explosion;
    [SerializeField] private AudioSource explosionSound;

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
        if (!_UseAltBehavior)
        {
            Debug.Log("Boomy Go!");
            Instantiate(explosion, transform.position, transform.rotation);
            explosionSound.Play();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Boomy is currently disarmed.");
        }
    }

    public void Toggle()
    {
        Debug.Log("Boomy Toggle!");
        _UseAltBehavior = !_UseAltBehavior;
    }

    public void Spin(bool AlternateBehavior)
    {
        
    }
}
