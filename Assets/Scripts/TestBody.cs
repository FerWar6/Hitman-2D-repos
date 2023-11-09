using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBody : MonoBehaviour
{
    public CapsuleCollider2D arm1;
    public CapsuleCollider2D arm2;
    public CapsuleCollider2D leg1;
    public CapsuleCollider2D leg2;

    public SpriteRenderer sr;
    Vector2 player;

    public static bool dragingABody = false;

    bool isToggled = true;
    bool playerInRange = false;
    bool hasCollision = false;
    bool collisionWPlayer;
    bool bodyKilled = false;
    private void Start()
    {
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
                if (ManageScene.currentEnemy == this.gameObject)
                    ManageScene.ReleaseCurrentEnemy();
                PlayerMovement.dragBody = false;
            }
            else 
            {
                if (!ManageScene.draggingEnemy)
                    ManageScene.SetCurrentEnemy(this.gameObject);
                PlayerMovement.dragBody = true;
            }
        }
        #region UpdateCollisionCheck
        if (!hasCollision)
        {
            sr.color = Color.red;
            playerInRange = false;
        }
        #endregion
    }
    private void UpdateCollisionState()
    {
        Collider2D innerPlayerTag = GameObject.FindGameObjectWithTag("InnerPlayer").GetComponent<Collider2D>();
        collisionWPlayer = arm1.IsTouching(innerPlayerTag) || arm2.IsTouching(innerPlayerTag) || leg1.IsTouching(innerPlayerTag) || leg2.IsTouching(innerPlayerTag);

        if (collisionWPlayer)
        {
            sr.color = Color.green;
        }
        else
        {
            sr.color = Color.red;
        }
    }

    void FixedUpdate()
    {
        UpdateCollisionState();
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