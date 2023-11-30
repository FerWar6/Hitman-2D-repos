using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageBox : MonoBehaviour
{
    PlayerMovement playerMovement;

    public SpriteRenderer sr;
    public BoxCollider2D extBoxColl;
    public Transform exitLocation;

    bool inThisBox = false;
    bool collisionWPlayer;

    int bodies;

    void Start()
    {
        sr.color = Color.red;

        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && ManageScene.currentEnemy != null && bodies < 2)
        {
            DestroyBody();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            // Get In Box
            if (!inThisBox && bodies < 2 && collisionWPlayer)
            {
                GetInBox();
                inThisBox = true;
            }
            // Get Out Of Box
            else if (inThisBox)
            {
                GetOutBox();
                inThisBox = false;
            }
        }
    }

    private void UpdateCollisionState()
    {
        collisionWPlayer = extBoxColl.IsTouching(GameObject.FindGameObjectWithTag("InnerPlayer").GetComponent<Collider2D>());

        if (collisionWPlayer && bodies < 2)
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
    public void GetInBox()
    {
        PlayerMovement.inBox = true;
        PlayerMovement.dragBody = false;
        playerMovement.SetPosition(transform);

        if (!ManageScene.hidingInBox)
            ManageScene.SetCurrentBox(this);
        ManageScene.hidingInBox = true;
        if (ManageScene.chokingEnemy)
            ManageScene.ReleaseCurrentChoke();
        if (ManageScene.draggingEnemy)
            ManageScene.ReleaseCurrentChoke();
    }
    public void GetOutBox()
    {
        PlayerMovement.inBox = false;
        playerMovement.SetPosition(exitLocation);

        if (ManageScene.currentBox == this)
            ManageScene.ReleaseCurrentBox();

    }
    public void DestroyBody()
    {
        Destroy(ManageScene.currentEnemy);
        ManageScene.draggingEnemy = false;
        PlayerMovement.dragBody = false;
        bodies++;
    }
}
