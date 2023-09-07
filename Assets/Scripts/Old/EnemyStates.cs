using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStates : MonoBehaviour
{
    #region Variables
    public BoxCollider2D enemyBox;
    public BoxCollider2D backBox;
    public Sprite choke;
    public Sprite dead;
    SpriteRenderer sr;


    bool killable = false;
    private float health = 10;
    #endregion
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(health <= 0)
        {
            sr.sprite = dead;
            enemyBox.enabled = false;
            backBox.enabled = false;
            killable = false;
            PlayerMovement.lockMovement = false;
        }
        if (Input.GetKeyDown("q") && killable)
        {

            StartCoroutine(ChokingEnemy());
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CheckBox"))
        {
            killable = true;
        }
        else
        {
            killable = false;
        }
    }
    IEnumerator ChokingEnemy()
    {

        sr.sprite = choke;
        for (int i = 0; i < 10; i++)
        {
            PlayerMovement.lockMovement = true;
            if (Input.GetKeyDown("q"))
            {
                yield return new WaitForSeconds(0.001f);
                health = health - 1;
            }
        }
    }
}
