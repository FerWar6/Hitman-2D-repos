using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBackCheck : MonoBehaviour
{
    public bool playerInRange;
    public bool playerInRange1;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up);


        if (hit.collider != null && hit.collider.CompareTag("InnerPlayer"))
        {
            playerInRange = true;
        }
        
        else
        {
            playerInRange = false;
        }

        Debug.Log(playerInRange);


    }

}


