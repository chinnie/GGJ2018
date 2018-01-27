using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugRayCaster : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastDebugRay();
        }
    }

    private void CastDebugRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 30, Color.yellow);

        //Debug.Log(ray.origin);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.GetComponent<IRobot>() != null)
            {
                hit.collider.gameObject.GetComponent<IRobot>().TriggerAction();
            }
            else
            {
                Debug.Log("Not looking at any bot");
            }
        }

    }
}
