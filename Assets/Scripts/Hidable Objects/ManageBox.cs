using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageBox : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public SpriteRenderer sr;
    public BoxCollider2D extBoxColl;
    public Transform exitLocation;

    bool inThisBox = false;
    public bool collisionWPlayer;

    public int bodies;

    void Start()
    {
        sr.color = Color.red;

        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }
    private void UpdateCollisionState()
    {
        collisionWPlayer = extBoxColl.IsTouching(GameObject.FindGameObjectWithTag("InnerPlayer").GetComponent<Collider2D>());

        if (collisionWPlayer && bodies < 2)
        {
            VariableManager.SetTargetBox(this);
            sr.color = Color.green;
        }
        else if (!collisionWPlayer)
        {
            if(VariableManager.targetBox == this)
            {
                VariableManager.ReleaseBox();
            }
            sr.color = Color.red;
        }
    }

    void FixedUpdate()
    {
        UpdateCollisionState();
    }
}
