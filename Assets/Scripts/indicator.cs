using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indicator : MonoBehaviour
{
    public SpriteRenderer sr;

    void Update()
    {
        if (PlayerMovement.input == 0)
        {
            sr.color = Color.red;
        }
        if (PlayerMovement.input == 1)
        {
            sr.color = Color.green;
        }
        if (PlayerMovement.input == 2)
        {
            sr.color = Color.yellow;
        }
    }
}
