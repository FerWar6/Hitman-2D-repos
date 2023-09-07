using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyChecks : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gameObject.name == "Arm1-1" || gameObject.name == "Arm1-2")
        {
            if (collision.CompareTag("InnerPlayer"))
            {
                BodyManager.Arm1InRange = true;
            }
            else
            {
                BodyManager.Arm1InRange = false;
            }
        }
        else if (gameObject.name == "Arm2-1" || gameObject.name == "Arm2-2")
        {
            if (collision.CompareTag("InnerPlayer"))
            {
                BodyManager.Arm2InRange = true;
            }
            else
            {
                BodyManager.Arm2InRange = false;
            }
        }
        else if (gameObject.name == "Leg1-2")
        {
            if (collision.CompareTag("InnerPlayer"))
            {
                BodyManager.Leg1InRange = true;
            }
            else
            {
                BodyManager.Leg1InRange = false;
            }
        }
        else if (gameObject.name == "Leg2-2")
        {
            if (collision.CompareTag("InnerPlayer"))
            {
                BodyManager.Leg2InRange = true;
            }
            else
            {
                BodyManager.Leg2InRange = false;
            }
        }
    }
}
