using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStates : MonoBehaviour
{
    float enemyHealth = 10;
    float distanceToPlayer;
    float angle;
    float desiredDistance = 0.25f;

    public bool DebugEnemy = true;

    bool isChoking = false;
    public BoxCollider2D enemyHitBox;

    Vector2 player;

    PlayerMovement playerMovement;
    Transform playerTransform;

    public GameObject Alive;
    public GameObject KnockedOut;
    public GameObject Unconscious;
    public GameObject Dead;
    public GameObject Body;

    public SpriteRenderer enemySprite;

    public int StartingState;

    public EnemyState currentState = EnemyState.Alive;
    void Start()
    {
        switch (StartingState)
        {
            case 1:
                currentState = EnemyState.Alive;
                break;
            case 2:
                currentState = EnemyState.KnockedOut;
                break;
            case 3:
                currentState = EnemyState.Unconscious;
                break;
            case 4:
                currentState = EnemyState.Dead;
                break;
            default:
                currentState = EnemyState.Alive;
                break;
        }
        UpdateState();
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            currentState = (EnemyState)(((int)currentState + 1) % System.Enum.GetValues(typeof(EnemyState)).Length);
            UpdateState();
        }

        RunState(currentState);
    }

    public void UpdateState()
    {
        switch (currentState)
        {
            case EnemyState.Alive:
                Alive.SetActive(true);
                Body.SetActive(false);

                KnockedOut.SetActive(false);
                Unconscious.SetActive(false);
                Dead.SetActive(false);

                enemySprite.enabled = true;
                break;

            case EnemyState.KnockedOut:
                KnockedOut.SetActive(true);
                Body.SetActive(true);

                Alive.SetActive(false);
                Unconscious.SetActive(false);
                Dead.SetActive(false);

                enemySprite.enabled = false;
                break;

            case EnemyState.Unconscious:
                Unconscious.SetActive(true);
                Body.SetActive(true);

                Alive.SetActive(false);
                KnockedOut.SetActive(false);
                Dead.SetActive(false);

                enemySprite.enabled = false;
                break;

            case EnemyState.Dead:
                Dead.SetActive(true);
                Body.SetActive(true);

                Alive.SetActive(false);
                KnockedOut.SetActive(false);
                Unconscious.SetActive(false);

                enemySprite.enabled = false;
                break;
        }
    }
    void RunState(EnemyState state)
    {
        if(state == EnemyState.Alive)
        {
            player = GameObject.FindWithTag("Player").transform.position;

            int playerLayerMask = ~(1 << gameObject.layer | 1 << LayerMask.NameToLayer("Player"));
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 0.35f, playerLayerMask);

            if (hit.collider != null && hit.collider.CompareTag("InnerPlayer"))
            {
                if (DebugEnemy)
                {
                    Debug.DrawRay(transform.position, -transform.up * 0.35f, Color.green);
                    Debug.DrawLine(transform.position, player, Color.blue);
                }
                ManageScene.SetCurrentChokeEnemy(this);

                angle = Mathf.Atan2(player.y - transform.position.y, player.x - transform.position.x) * Mathf.Rad2Deg;
                playerMovement.SetChokeAngle(angle);
            }
            else
            {
                if (DebugEnemy)
                {
                    Debug.DrawRay(transform.position, -transform.up * 0.35f, Color.red);
                }
            }
        }
        if (state == EnemyState.KnockedOut)
        {

        }
        if(state == EnemyState.Unconscious)
        {

        }
        if(state == EnemyState.Dead)
        {

        }
    }
    public void ChangeState(EnemyState newState)
    {
        currentState = newState;
        UpdateState();
    }
    public enum EnemyState
    {
        Alive,
        KnockedOut,
        Unconscious,
        Dead
    }
}
