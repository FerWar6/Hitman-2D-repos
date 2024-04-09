using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableManager : MonoBehaviour
{
    public static bool isChoking = false;
    public static bool isDraging = false;
    public static bool isHiding = false;
    public static bool isThrowing = false;

    public static bool canChoke = false;
    public static bool canDrag = false;
    public static bool canHide = false;
    public static bool canThrow = false;

    public static EnemyStates targetChoke;
    public static EnemyStates choke;

    public static EnemyStates targetBody;
    public static EnemyStates body;

    public static EnemyStates targetThrow;
    public static EnemyStates throwAt;

    public static ManageBox targetBox;
    public static ManageBox box;

    #region Setting Choke
    public static void SetTargetChoke(EnemyStates target)
    {
        targetChoke = target;
        canChoke = true;
    }
    public static void SetChokeAsTargetChoke(EnemyStates target)
    {
        choke = target;
        canChoke = false;
        isChoking = true;
    }
    public static void ReleaseTargetChoke()
    {
        targetChoke = null;
        canChoke = false;
    }    
    public static void ReleaseChoke()
    {
        targetChoke = null;
        choke = null;
        canChoke = false;
        isChoking = false;
    }
    #endregion
    #region Setting Body
    public static void SetTargetBody(EnemyStates target)
    {
        targetBody = target;
        canDrag = true;
    }
    public static void SetBodyAsTargetBody(EnemyStates target)
    {
        body = target;
        canDrag = false;
        isDraging = true;
    }
    public static void ReleaseTargetBody()
    {
        targetBody = null;
        canDrag = false;
    }
    public static void ReleaseBody()
    {
        targetBody = null;
        body = null;
        canDrag = false;
        isDraging = false;
    }
    public static void DestroyBody()
    {
        Destroy(body);
        isDraging = false;
        box.bodies++;
    }

    #endregion    
    #region Setting Throw
    public static void SetTargetThrow(EnemyStates target)
    {
        targetThrow = target;
        canThrow = true;
    }
    public static void SetThrowAsTargetThrow(EnemyStates target)
    {
        throwAt = target;
        canThrow = true;
        isThrowing = true;
    }
    public static void ReleaseTargetThrow()
    {
        targetThrow = null;
        canThrow = true;
    }
    public static void ReleaseThrow()
    {
        targetThrow = null;
        throwAt = null;
        canThrow = true;
        isThrowing = false;
    }
    #endregion    
    #region Setting Box
    public static void SetTargetBox(ManageBox target)
    {
        targetBox = target;
        canHide = true;
    }
    public static void SetBoxAsTargetBox(ManageBox target)
    {
        box = target;
        canHide = false;
        isHiding = true;
    }
    public static void ReleaseTargetBox()
    {
        targetBox = null;
        isHiding = false;
    }
    public static void ReleaseBox()
    {
        targetBox = null;
        box = null;
        canHide = false;
        isHiding = false;
    }
#endregion
    private void Update()
    {
        Debug.Log("Current Target enemy" + targetChoke);
        Debug.Log("Current Target box" + targetBox);
        if(choke != null)
        {
            Debug.Log("Choking enemy" + choke);
        }
    }
}
