using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    int bodies;
    public bool ableToHideBody = false;
    public static bool destoyBody = false;
    private void Update()
    {
        if (bodies == 2)
        {
            ableToHideBody = false;
        }


    }
}
