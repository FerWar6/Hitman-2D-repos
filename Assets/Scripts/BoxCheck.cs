using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCheck : MonoBehaviour
{
    PlayerMovement playerMovement;

    public BoxManager BoxMan;
    bool ableToHide = false;
    bool inThisBox = false;

    public Transform exitLocation;


    SpriteRenderer sr;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.red;
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }
    private bool hasCollision = false;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("InnerPlayer"))
        {
            BoxMan.ableToHideBody = true;
            ableToHide = true;
            sr.color = Color.green;
            hasCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InnerPlayer"))
        {
            BoxMan.ableToHideBody = false;
            ableToHide = false;
            sr.color = Color.red;
            hasCollision = false;
        }
    }

    private void Update()
    {
        if (!hasCollision)
        {
            BoxMan.ableToHideBody = false;
            ableToHide = false;
            sr.color = Color.red;
        }


    }
}
