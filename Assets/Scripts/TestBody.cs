using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBody : MonoBehaviour
{
    public CapsuleCollider2D bodyPart1;
    public CapsuleCollider2D bodyPart2;
    public CapsuleCollider2D bodyPart3;
    public CapsuleCollider2D bodyPart4;

    public SpriteRenderer sr;
    Vector2 player;

    public static bool dragingABody = false;

    bool isToggled = true;
    bool playerInRange = false;
    bool hasCollision = false;
    bool collisionWbodyPart1;
    bool collisionWbodyPart2;
    bool collisionWbodyPart3;
    bool collisionWbodyPart4;
    bool bodyKilled = false;
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
        CheckCollisionWithInnerPlayer(bodyPart1);
        CheckCollisionWithInnerPlayer(bodyPart2);
        CheckCollisionWithInnerPlayer(bodyPart3);
        CheckCollisionWithInnerPlayer(bodyPart4);
    }



    void CheckCollisionWithInnerPlayer(CapsuleCollider2D collider)
    {
        if (collider.IsTouching(GameObject.FindGameObjectWithTag("InnerPlayer").GetComponent<Collider2D>()))
        {
            // Do something when the collider is in collision with InnerPlayer
            Debug.Log($"{collider.name} is in collision with InnerPlayer");
        }
    }
    private void UpdateCollisionState()
    {
        CircleCollider2D innerPlayerCollider = GameObject.FindGameObjectWithTag("InnerPlayer").GetComponent<CircleCollider2D>();

        if (bodyPart1.IsTouching(innerPlayerCollider))
        {
            // Collision detected
            Debug.Log("InnerPlayer collided with bodyPart1");
        }
                float radius = bodyPart1.bounds.size.x / 2f;  // Assuming the collider is roughly circular

                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask.GetMask("InnerPlayer"));


                // Check if there are any colliders with the InnerPlayer tag
                bool playerCollision = colliders.Length > 0;

                if (playerCollision)
                {
                    float distance1 = Vector3.Distance(bodyPart1.gameObject.transform.position, colliders[0].gameObject.transform.position);
                    float distance2 = Vector3.Distance(bodyPart2.gameObject.transform.position, colliders[0].gameObject.transform.position);
                    float distance3 = Vector3.Distance(bodyPart3.gameObject.transform.position, colliders[0].gameObject.transform.position);
                    float distance4 = Vector3.Distance(bodyPart4.gameObject.transform.position, colliders[0].gameObject.transform.position);

                    float shortestDistance = Mathf.Min(distance1, Mathf.Min(distance2, Mathf.Min(distance3, distance4)));
                    if (shortestDistance == distance1)
                    {
                        Debug.Log("1");
                    }
                    if (shortestDistance == distance2)
                    {
                        Debug.Log("2");
                    }
                    if (shortestDistance == distance3)
                    {
                        Debug.Log("3");
                    }
                    if (shortestDistance == distance4)
                    {
                        Debug.Log("4");
                    }
                }
    }



    void FixedUpdate()
    {
        UpdateCollisionState();
    }
    #region Manage PlayerInRange
    public bool touching1 = false;
    public bool toucing2 = false;
    public bool toucing3 = false;
    public bool toucing4 = false;
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("InnerPlayer"))
        {
            if (collision.IsTouching(bodyPart1))
            {
                Debug.Log($"{gameObject.name} is in collision with InnerPlayer at bodyPart1");
                touching1 = true;
            }

            if (collision.IsTouching(bodyPart2))
            {
                Debug.Log($"{gameObject.name} is in collision with InnerPlayer at bodyPart2");
                toucing2 = true;
            }

            if (collision.IsTouching(bodyPart3))
            {
                Debug.Log($"{gameObject.name} is in collision with InnerPlayer at bodyPart3");
                toucing3 = true;
            }

            if (collision.IsTouching(bodyPart4))
            {
                Debug.Log($"{gameObject.name} is in collision with InnerPlayer at bodyPart4");
                toucing4 = true;
            }
            if (collision.CompareTag("InnerPlayer"))
            {
                playerInRange = true;
                hasCollision = true;
                sr.color = Color.green;
            }
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