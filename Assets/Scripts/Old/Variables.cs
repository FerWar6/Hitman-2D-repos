using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{
    [System.Serializable]
    public class EnemyVariables
    {
        public Sprite choke;
        public BoxCollider2D enemyHitBox;
        public GameObject Body;
    }
    public EnemyVariables variables;
}
