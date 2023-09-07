using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleStates : MonoBehaviour
{
    public SpriteRenderer sr;
    public Material Stabable;
    public Material Normal;

    private void Update()
    {
        if (HitmanInstaKill.colorChange)
        {
            sr.material = Stabable;
        }
        else
        {
            sr.material = Normal;
        }
    }

}
