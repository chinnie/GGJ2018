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
                //var intersection = hit.point;

                //var bots = GameObject.FindGameObjectsWithTag("Bot");

                //// Call toggle on all bots but this one
                //GameObject closestbot = null;

                //foreach (var bot in bots)
                //{
                //    if (bot.GetComponent<IRobot>() != null)
                //    {
                //        if (closestbot == null)
                //        {
                //            closestbot = bot;
                //        }
                //        else
                //        {
                //            var botdis = Vector3.Distance(bot.transform.position, intersection);
                //            var closedis = Vector3.Distance(closestbot.transform.position, intersection);

                //            if (botdis < closedis)
                //            {
                //                closestbot = bot;
                //            }
                //        }
                        
                //    }
                //}
                //Debug.Log(intersection + " :: " + closestbot.transform.position);
                //closestbot.GetComponent<IRobot>().TriggerAction();
            }
        }

    }
}
