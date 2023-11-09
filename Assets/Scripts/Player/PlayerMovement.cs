using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    public static bool inBox = false;
    public static TestBody currentBox;

    public static int input = 0;

    Vector2 boxPosition;
    float chokeAngle;
    float sprintExel1 = 0.085f;
    float sprintExel2 = 0.1f;
    float slowSpeed = 1.07f;
    float extraSlowSpeed = 1.12f;

    public static bool getOutOfBox = false;
    public static bool dragBody = false;
    public static bool lockMovement = false;
    bool idle = true;
    bool moveDiagonal = false;

    Rigidbody2D rb2D;
    SpriteRenderer sr;

    public Sprite dragBodySprite;
    public Sprite normalSprite;
    #endregion

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        #region ManageVelocity
        bool underMaxVelocity = true;
        if (rb2D.velocity.x < 2 && rb2D.velocity.y < 2 && rb2D.velocity.x > -2 && rb2D.velocity.y > -2)
        {
            underMaxVelocity = true;
        }
        else
        {
            underMaxVelocity = false;
        }
        #endregion
        #region InputManager
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
        {
            idle = false;
            moveDiagonal = true;
            input = 2;
        }
        else if (Input.GetKey(KeyCode.W) && !moveDiagonal || Input.GetKey(KeyCode.A) && !moveDiagonal || Input.GetKey(KeyCode.S) && !moveDiagonal || Input.GetKey(KeyCode.D) && !moveDiagonal)
        {
            idle = false;
            input = 1;
        }
        else
        {
            moveDiagonal = false;
            idle = true;
            input = 0;
        }
        #endregion
        #region Sprint Horizontal Vertical
        if (Input.GetKey(KeyCode.W) && input == 1 && !lockMovement && underMaxVelocity)
        {
            if (rb2D.velocity.y < 1.2f)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x / extraSlowSpeed, rb2D.velocity.y + sprintExel1);
            }
            else if (rb2D.velocity.y > 1.2f && rb2D.velocity.y < 2)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x / extraSlowSpeed, rb2D.velocity.y + sprintExel2);
            }
        }
        else if (Input.GetKey(KeyCode.A) && input == 1 && !lockMovement && underMaxVelocity)
        {
            if (rb2D.velocity.x > -1.2f)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x - sprintExel1, rb2D.velocity.y / extraSlowSpeed);
            }
            else if (rb2D.velocity.x < -1.2f && rb2D.velocity.x > -2)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x - sprintExel2, rb2D.velocity.y / extraSlowSpeed);
            }
        }
        else if (Input.GetKey(KeyCode.S) && input == 1 && !lockMovement && underMaxVelocity)
        {
            if (rb2D.velocity.y > -1.2f)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x / extraSlowSpeed, rb2D.velocity.y - sprintExel1);
            }
            else if (rb2D.velocity.y < -1.2f && rb2D.velocity.y > -2)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x / extraSlowSpeed, rb2D.velocity.y - sprintExel2);
            }
        }
        else if (Input.GetKey(KeyCode.D) && input == 1 && !lockMovement && underMaxVelocity)
        {
            if (rb2D.velocity.x < 1.2f)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x + sprintExel1, rb2D.velocity.y / extraSlowSpeed);
            }
            else if (rb2D.velocity.x > 1.2f && rb2D.velocity.x < 2)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x + sprintExel2, rb2D.velocity.y / extraSlowSpeed);
            }
        }
        #endregion
        #region Sprint Diagonal
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && input == 2 && !lockMovement && underMaxVelocity)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x - 0.05f, rb2D.velocity.y + 0.05f);
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && input == 2 && !lockMovement && underMaxVelocity)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x - 0.05f, rb2D.velocity.y - 0.05f);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) && input == 2 && !lockMovement && underMaxVelocity)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x + 0.05f, rb2D.velocity.y - 0.05f);
        }
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W) && input == 2 && !lockMovement && underMaxVelocity)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x + 0.05f, rb2D.velocity.y + 0.05f);
        }
        #endregion
        #region Manage Bools
        if (idle)
        {
            rb2D.velocity = rb2D.velocity / slowSpeed;
        }

        if (lockMovement)
        {
            sr.sprite = dragBodySprite;
            transform.localRotation = Quaternion.Euler(0, 0, chokeAngle + 90);
            rb2D.velocity = new Vector2(0, 0);
        }

        if (dragBody)
        {
            sr.sprite = dragBodySprite;
            sprintExel1 = 0.04f;
            sprintExel2 = 0.05f;
        }
        else if (!dragBody && !lockMovement)
        {
            sr.sprite = normalSprite;
        }
        else
        {
            sprintExel1 = 0.085f;
            sprintExel2 = 0.1f;
        }
        if (getOutOfBox)
        {
            transform.position = boxPosition;
            getOutOfBox = false;
        }
        if (inBox)
        {
            sr.enabled = false;
            rb2D.simulated = false;
            lockMovement = true;
        }
        else
        {
            sr.enabled = true;
            rb2D.simulated = true;
            lockMovement = false;
        }
        #endregion
    }
    public void SetPosition(Transform position)
    {
        transform.position = (position.position);
    }
    public void PositionGetOutOfBox(Vector2 position)
    {
        boxPosition = position;
    }
    public void SetChokeAngle(float angle)
    {
        chokeAngle = angle;
    }
}
