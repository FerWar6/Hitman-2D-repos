using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCheck : MonoBehaviour
{
    PlayerMovement playerMovement;

    public BoxManager BoxMan;
    bool ableToHide = false;
    bool isToggled = true;


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
        if (Input.GetKeyDown(KeyCode.H) && ableToHide)
        {
            isToggled = !isToggled;
            if (isToggled)
            {
                if (ManageScene.currentEnemy == this)
                    ManageScene.ReleaseCurrentBox();
                PlayerMovement.dragBody = false;
            }
            else
            {
                PlayerMovement.getOutOfBox = true;
                playerMovement.PositionGetOutOfBox(new Vector2(transform.position.x, transform.position.y));

                if (!ManageScene.draggingEnemy)
                    ManageScene.SetCurrentBox(this);
                PlayerMovement.dragBody = true;
            }
        }
            Debug.Log(isToggled);
            Debug.Log(ManageScene.currentBox);
    }
}
