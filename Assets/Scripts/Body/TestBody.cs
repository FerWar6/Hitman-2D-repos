using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBody : MonoBehaviour
{
    Vector2 player;

    Collider2D innerPlayerCollider;

    bool isToggled = true;
    bool toucingPlayer = false;

    Transform BodyPart1;
    Transform BodyPart2;
    Transform BodyPart3;
    Transform BodyPart4;

    Transform closestBodyPart = null;

    DetectPlayerCollision detectPlayerCollision1;
    DetectPlayerCollision detectPlayerCollision2;
    DetectPlayerCollision detectPlayerCollision3;
    DetectPlayerCollision detectPlayerCollision4;
    private void Start()
    {
        #region Find And Set Colliders
        BodyPart1 = transform.Find("BodyPart1");
        BodyPart2 = transform.Find("BodyPart2");
        BodyPart3 = transform.Find("BodyPart3");
        BodyPart4 = transform.Find("BodyPart4");

        detectPlayerCollision1 = BodyPart1?.GetComponent<DetectPlayerCollision>();
        detectPlayerCollision2 = BodyPart2?.GetComponent<DetectPlayerCollision>();
        detectPlayerCollision3 = BodyPart3?.GetComponent<DetectPlayerCollision>();
        detectPlayerCollision4 = BodyPart4?.GetComponent<DetectPlayerCollision>();
        #endregion
        GameObject innerPlayer = GameObject.FindWithTag("InnerPlayer");
        if (innerPlayer != null)
        {
            innerPlayerCollider = innerPlayer.GetComponent<Collider2D>();
        }
    }
    void Update()
    {
        toucingPlayer = detectPlayerCollision1.collisionWithPlayer || detectPlayerCollision2.collisionWithPlayer || detectPlayerCollision3.collisionWithPlayer || detectPlayerCollision4.collisionWithPlayer;

        player = GameObject.FindWithTag("Player").transform.position;


        if (ManageScene.currentEnemy != null && ManageScene.currentEnemy.gameObject == gameObject)
        {
            if(closestBodyPart != null)
            {
                Vector3 playerPos = new Vector3(player.x, player.y, -0.1f);
                closestBodyPart.position = Vector2.MoveTowards(closestBodyPart.position, playerPos, 5);
            }
            else
            {
                Debug.Log("no bodypart found");
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (ManageScene.currentEnemy == this.gameObject)
            {
                ManageScene.ReleaseCurrentEnemy();
                PlayerMovement.dragBody = false;
                closestBodyPart = null;
                isToggled = false;
            }
            else if (!ManageScene.draggingEnemy && toucingPlayer)
            {
                ManageScene.SetCurrentEnemy(this.gameObject);
                PlayerMovement.dragBody = true;
                closestBodyPart = CalculateBodyPart();
                isToggled = true;
            }
        }
    }

    private Transform CalculateBodyPart() 
    {
        Transform bodypart = null;
        float distance1 = Vector3.Distance(detectPlayerCollision1.gameObject.transform.position, innerPlayerCollider.gameObject.transform.position);
        float distance2 = Vector3.Distance(detectPlayerCollision2.gameObject.transform.position, innerPlayerCollider.gameObject.transform.position);
        float distance3 = Vector3.Distance(detectPlayerCollision3.gameObject.transform.position, innerPlayerCollider.gameObject.transform.position);
        float distance4 = Vector3.Distance(detectPlayerCollision4.gameObject.transform.position, innerPlayerCollider.gameObject.transform.position);

        float shortestDistance = Mathf.Min(distance1, Mathf.Min(distance2, Mathf.Min(distance3, distance4)));
        if (shortestDistance == distance1)
        {
            bodypart = detectPlayerCollision1.GrabLocation.transform;
            return bodypart;
        }
        else if (shortestDistance == distance2)
        {
            bodypart = detectPlayerCollision2.GrabLocation.transform;
            return bodypart;
        }
        else if (shortestDistance == distance3)
        {
            bodypart = detectPlayerCollision3.GrabLocation.transform;
            return bodypart;
        }
        else if (shortestDistance == distance4)
        {
            bodypart = detectPlayerCollision4.GrabLocation.transform;
            return bodypart;
        }
        return null;
    }
}