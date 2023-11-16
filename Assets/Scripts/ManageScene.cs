using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageScene : MonoBehaviour
{
    public TMP_Text debug;

    public static bool targetFound = false;
    public static EnemyStates TargetedEnemy;
    public static bool canChoke = false;
    public static EnemyStates TargetedChokeEnemy;
    
    public static bool foundTarget = false;
    public static EnemyAttacked TargetEnemy;
    public static bool chokingEnemy = false;
    public static EnemyAttacked currentChoke;
    public static bool draggingEnemy = false;
    public static GameObject currentEnemy;
    public static bool hidingInBox = false;
    public static ManageBox currentBox;
    public static void SetCurrentEnemy(GameObject enemy)
    {
        currentEnemy = enemy;
        draggingEnemy = true;
    }

    public static void ReleaseCurrentEnemy()
    {
        currentEnemy = null;
        draggingEnemy = false;
    }
    public static void SetCurrentBox(ManageBox box)
    {
        currentBox = box;
        hidingInBox = true;
    }

    public static void ReleaseCurrentBox()
    {
        currentBox = null;
        hidingInBox = false;
    }
    public static void SetCurrentChoke(EnemyAttacked choke)
    {
        currentChoke = choke;
        chokingEnemy = true;
    }

    public static void ReleaseCurrentChoke()
    {
        currentChoke = null;
        chokingEnemy = false;
    }
    public static void SetCurrentTarget(EnemyAttacked target)
    {
        TargetEnemy = target;
        foundTarget = true;
    }

    public static void ReleaseCurrentTarget()
    {
        TargetEnemy = null;
        foundTarget = false;
    }





    public static void SetCurrentChokeEnemy(EnemyStates choke)
    {
        TargetedChokeEnemy = choke;
        canChoke = true;
    }

    public static void ReleaseCurrentChokeEnemy()
    {
        TargetedChokeEnemy = null;
        canChoke = false;
    }
    public static void SetCurrentTargetedEnemy(EnemyStates target)
    {
        TargetedEnemy = target;
        targetFound = true;
    }

    public static void ReleaseCurrentTargetedEnemy()
    {
        TargetedEnemy = null;
        targetFound = false;
    }
}
