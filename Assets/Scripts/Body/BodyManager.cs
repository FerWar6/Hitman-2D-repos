using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : MonoBehaviour
{
    Vector2 player;

    public Rigidbody2D rb2D1;
    public Rigidbody2D rb2D2;
    public Rigidbody2D rb2D3;

    public static bool Arm1InRange;
    public static bool Arm2InRange;
    public static bool Leg1InRange;
    public static bool Leg2InRange;
    void Update()
    {
        player = GameObject.FindWithTag("Player").transform.position;
        if (Input.GetKey(KeyCode.T))
        {
            Debug.Log("Crayz hambuter");
            rb2D1.velocity = new Vector2(0.1f, 0.1f);
            rb2D2.velocity = new Vector2(0.1f, 0.1f);
            rb2D3.velocity = new Vector2(0.1f, 0.1f);
            //    transform.position = Vector2.MoveTowards(transform.position, player, 0.1f * Time.deltaTime);
        }
/*        Debug.Log(Arm1InRange);
        Debug.Log(Arm2InRange);
        Debug.Log(Leg1InRange);
        Debug.Log(Leg2InRange);*/
    }
}
