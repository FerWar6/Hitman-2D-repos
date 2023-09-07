using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BodyUIManager : MonoBehaviour
{
    public EnemyAttacked enemyScript;
    public TestBody body;
    public int UIid = 0;
    TextMeshProUGUI UI;


    private void Start()
    {
        UI = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(UIid == 1)
        {
            if (enemyScript.isChoking)
            {
                UI.enabled = true;
            }
            else
            {
                UI.enabled = false;
            }
        }
        if (UIid == 2)
        {
            if (enemyScript.isChoking)
            {
                UI.enabled = true;
            }
            else
            {
                UI.enabled = false;
            }
        }
    }
}
