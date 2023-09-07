using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacked : MonoBehaviour
{
    #region Variables

    public Sprite choke;
    public BoxCollider2D enemyHitBox;
    public GameObject Body;

    SpriteRenderer sr;
    PlayerMovement playerMovement;
    Transform playerTransform;

    Vector3 moveDirection;
    Vector2 player;

    bool ableToAttack = false;
    public bool isChoking = false;

    float distanceToPlayer;
    float angle;
    float health = 10;
    float desiredDistance = 0.25f;
    #endregion
    #region Variable Setup
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    #endregion
    private void Update()
    {
        #region Dying
        if (health <= 0)
        {
            PlayerMovement.lockMovement = false;
            GameObject deadBody = Instantiate(Body);
            deadBody.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            deadBody.transform.position = new Vector2(transform.position.x,transform.position.y);
            Destroy(gameObject);
        }
        #endregion
        #region Being Attacked
        player = GameObject.FindWithTag("Player").transform.position;
        if (Input.GetKeyDown("q") && ableToAttack && !isChoking && !ManageScene.chokingEnemy)
        {
            ManageScene.SetCurrentChoke(this);
            StartCoroutine(ChokingEnemy());
        }
        #endregion
        #region RaycastChecks
        int playerLayerMask = ~(1 << gameObject.layer| 1 << LayerMask.NameToLayer("Player"));
        Color red = Color.red;
        Color green = Color.green;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 0.35f,playerLayerMask);

        if (hit.collider != null && hit.collider.CompareTag("InnerPlayer"))
        {
            Debug.DrawRay(transform.position, -transform.up * 0.35f, green);
            Debug.DrawLine(transform.position, player, Color.blue);
            angle = Mathf.Atan2(player.y - transform.position.y, player.x - transform.position.x) * Mathf.Rad2Deg;
            playerMovement.SetChokeAngle(angle);
            ableToAttack = true;
        }
        else
        {
            Debug.DrawRay(transform.position, -transform.up * 0.35f, red);
            ableToAttack = false;
        }
        #endregion
    }

    #region  Choking Enemy IEnumerator
    IEnumerator ChokingEnemy()
    {
        isChoking = true;
        bool isFirstIteration = true;

        while (true)
        {
            Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
            distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            Vector2 targetPosition = (Vector2)transform.position + directionToPlayer * desiredDistance;

            if (isFirstIteration)
            {
                enemyHitBox.enabled = false;
                transform.localRotation = Quaternion.Euler(0, 0, angle + 90);
                PlayerMovement.lockMovement = true;
                transform.position = new Vector2(targetPosition.x, targetPosition.y);
                sr.sprite = choke;
                isFirstIteration = false;
            }

            if (Input.GetKeyDown("q"))
            {
                yield return new WaitForSeconds(0.001f);
                health--;
            }

            if (health <= 0)
            {
                break; 
            }

            yield return null;
        }
        if (ManageScene.currentChoke == this)
            ManageScene.ReleaseCurrentChoke ();
        isChoking = false;
        PlayerMovement.lockMovement = false;
    }
    #endregion
}
