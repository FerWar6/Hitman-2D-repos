using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageScene : MonoBehaviour
{
    public static bool chokingEnemy = false;
    public static EnemyAttacked currentChoke;
    public static bool draggingEnemy = false;
    public static TestBody currentEnemy;
    public static bool hidingInBox = false;
    public static BoxCheck currentBox;
    public static void SetCurrentEnemy(TestBody enemy)
    {
        currentEnemy = enemy;
        draggingEnemy = true;
    }

    public static void ReleaseCurrentEnemy()
    {
        currentEnemy = null;
        draggingEnemy = false;
    }
    public static void SetCurrentBox(BoxCheck box)
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
}
