using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class DrawHitbox : MonoBehaviour
{
    private CapsuleCollider2D capsuleCollider;

    private void OnDrawGizmos()
    {
        if (capsuleCollider == null)
        {
            capsuleCollider = GetComponent<CapsuleCollider2D>();
        }

        if (capsuleCollider != null)
        {
            DrawHitboxGizmo();
        }
    }

    private void DrawHitboxGizmo()
    {
        Gizmos.color = Color.green;

        Vector2 size = new Vector2(capsuleCollider.size.x, capsuleCollider.size.y);
        Vector2 center = (Vector2)transform.position + capsuleCollider.offset;

        float halfHeight = size.y * 0.5f;
        float radius = size.x * 0.5f;

        // Draw the top circle
        Gizmos.DrawWireSphere(center + new Vector2(0, halfHeight - radius), radius);

        // Draw the bottom circle
        Gizmos.DrawWireSphere(center - new Vector2(0, halfHeight - radius), radius);

        // Draw the vertical lines
        Gizmos.DrawLine(center + new Vector2(radius, halfHeight - radius), center + new Vector2(radius, -halfHeight + radius));
        Gizmos.DrawLine(center - new Vector2(radius, halfHeight - radius), center - new Vector2(radius, -halfHeight + radius));
    }
}
