using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthAttack : MonoBehaviour
{

    private void Update()
    {
        Vector3 backward = -transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, backward, out hit))
        {
            if (hit.collider.CompareTag("Player"))
            {

            }
        }
        Debug.DrawRay(transform.position, backward * 10f, Color.red);
    }
}
