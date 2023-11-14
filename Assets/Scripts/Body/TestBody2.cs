using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBody2 : MonoBehaviour
{
    Vector2 player;

    void Update()
    {
        player = GameObject.FindWithTag("Player").transform.position;
        if (Input.GetKey(KeyCode.T))
        {
            PlayerMovement.dragBody = true;
            transform.position = Vector2.MoveTowards(transform.position, player, 5);
        }
        else
        {
            PlayerMovement.dragBody = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gameObject.name == "Arm1-1" || gameObject.name == "Arm1-2")
        {
            if (collision.CompareTag("InnerPlayer"))
            {
                BodyManager.Arm1InRange = true;
            }
        }
    }
}