using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    bool aiming = false;
    bool throwableInHand = true;

    public LayerMask raycastIgnorePlayer;
    public GameObject Throwable;
    public LineRenderer lineRenderer;

    Vector2 endPos;

    EnemyAttacked enemyAttacked;

    private void Start()
    {
        lineRenderer.positionCount = 2;
    }
    void Update()
    {
        throwableInHand = InventorySystem.itemInHand.isThrowable;

        if (Input.GetMouseButton(1) && throwableInHand)
        {
            aiming = true;
            TrowingRaycast();
        }
        else
        {
            aiming = false;
        }
        if (!throwableInHand || !aiming)
        {
            HideLineRenderer();
        }

        if (Input.GetMouseButtonDown(0) && aiming)
        {
            if (!ManageScene.foundTarget)
            {
                TrowObject(endPos);
            }
            else if (ManageScene.foundTarget)
            {
                TrowAtTarget();
            }
        }
    }

    private void TrowingRaycast()
    {
        Color red = Color.red;
        Color green = Color.green;

        int layerMask = ~LayerMask.GetMask("Player", "InnerPlayer");

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float rayDistance = Vector3.Distance(transform.position, mousePosition);

        Vector2 direction = (mousePosition - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayDistance, layerMask);
        Vector2 throwPosition = hit.point;

        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            SetLinePositions(hit.collider.transform.position);
            EnemyAttacked enemyAttacked = hit.collider.gameObject.GetComponent<EnemyAttacked>();

            ManageScene.SetCurrentTarget(enemyAttacked);
        }
        else if (hit.collider != null && !hit.collider.CompareTag("Enemy") || hit.collider == null)
        {
            ManageScene.ReleaseCurrentTarget();
            endPos = mousePosition;
            SetLinePositions(mousePosition);
        }
    }
    private void TrowAtTarget()
    {
        if (InventorySystem.itemInHand.isLethal == true)
        {
            // set enemy state do dead
            Debug.Log("Threw lethal Object");
            ManageScene.TargetEnemy.gameObject.GetComponent<EnemyAttacked>().Die();
            ManageScene.ReleaseCurrentTarget();
        }        
        if (InventorySystem.itemInHand.isLethal == false)
        {
            // set enemy state do unconcious
            Debug.Log("Threw non lethal Object");
            ManageScene.TargetEnemy.gameObject.GetComponent<EnemyAttacked>().Die();
            ManageScene.ReleaseCurrentTarget();
        }

    }
    private void TrowObject(Vector2 throwPos)
    {
        GameObject tool = Instantiate(Throwable);
        tool.transform.position = throwPos;

    }
    private void SetLinePositions(Vector2 end)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, end);
    }
    private void HideLineRenderer()
    {
        lineRenderer.enabled = false;
    }
}
