using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    Vector2 player;
    public float range = 1;
    float timeDetected = 0;

    void Update()
    {
        float[] angles = { 45f, 22.5f, 0f, -22.5f, -45f };

        int playerLayerMask = ~(1 << gameObject.layer | 1 << LayerMask.NameToLayer("Player"));
        player = GameObject.FindWithTag("Player").transform.position;

        foreach (float angle in angles)
        {
            Vector3 rayDirection = Quaternion.Euler(0, 0, angle) * transform.up;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, range, playerLayerMask);

            if (hit.collider != null && hit.collider.CompareTag("InnerPlayer"))
            {
                Debug.DrawRay(transform.position, rayDirection * range, Color.red);
                Debug.DrawLine(transform.position, player, Color.blue);
            }
            else
            {
                Debug.DrawRay(transform.position, rayDirection * range, Color.green);
            }
        }
    }
}