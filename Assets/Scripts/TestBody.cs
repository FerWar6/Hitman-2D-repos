using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBody : MonoBehaviour
{
    SpriteRenderer sr;
    Vector2 player;

    public static bool dragingABody = false;

    bool isToggled = true;
    bool playerInRange = false;
    bool hasCollision = false;

    bool bodyKilled = false;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.red;
    }
    void Update()
    {
        player = GameObject.FindWithTag("Player").transform.position;

        if (ManageScene.currentEnemy != null && ManageScene.currentEnemy.gameObject == gameObject)
        {
            transform.position = Vector2.MoveTowards(transform.position, player, 5);
        }

        if (Input.GetKeyDown(KeyCode.F) && playerInRange)
        {
            isToggled = !isToggled;
            if (isToggled)
            {
                if (ManageScene.currentEnemy == this)
                    ManageScene.ReleaseCurrentEnemy();
                PlayerMovement.dragBody = false;
            }
            else 
            {
                if (!ManageScene.draggingEnemy)
                    ManageScene.SetCurrentEnemy(this);
                PlayerMovement.dragBody = true;
            }
        }
        #region Destroy Body
        if (BoxManager.destoyBody && ManageScene.currentEnemy != null && ManageScene.currentEnemy.gameObject == gameObject)
        {
            PlayerMovement.dragBody = false;
            ManageScene.ReleaseCurrentEnemy();
            Destroy(gameObject);
        }
        #endregion
        #region UpdateCollisionCheck
        if (!hasCollision)
        {
            sr.color = Color.red;
            playerInRange = false;
        }
        #endregion
    }
    #region Manage PlayerInRange
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("InnerPlayer"))
        {
            playerInRange = true;
            hasCollision = true;
            sr.color = Color.green;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InnerPlayer"))
        {
            hasCollision = false;
        }
    }
    #endregion
}