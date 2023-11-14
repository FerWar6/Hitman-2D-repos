using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerCollision : MonoBehaviour
{
    public Transform GrabLocation;
    public bool collisionWithPlayer;
    bool hasCollision = false;
    private void Update()
    {
        collisionWithPlayer = hasCollision;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("InnerPlayer"))
        {
            hasCollision = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InnerPlayer"))
        {
            hasCollision = false;
        }
    }
}
