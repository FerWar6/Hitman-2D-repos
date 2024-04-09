using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private bool isQPressed = false;
    public PlayerMovement playerMovement;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !VariableManager.isChoking)
        {
            if (VariableManager.targetChoke != null)
            {
                VariableManager.SetChokeAsTargetChoke(VariableManager.targetChoke);
                PlayerMovement.lockMovement = true;
                isQPressed = true;
                StartCoroutine(ChokeEnemy());
            }
        }

        if (Input.GetKeyDown(KeyCode.G) && VariableManager.body != null && VariableManager.box.bodies < 2)
        {
            VariableManager.DestroyBody();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (!VariableManager.isHiding && VariableManager.targetBox.bodies < 2 && VariableManager.targetBox.collisionWPlayer)
            {
                VariableManager.SetBoxAsTargetBox(VariableManager.targetBox);
                GetInBox();
            }
            else if (VariableManager.isHiding)
            {
                GetOutBox();
                VariableManager.ReleaseBox();
            }
        }
    }
    public void GetInBox()
    {
        Debug.Log("Getting In box");
        playerMovement.SetPosition(VariableManager.box.transform);
        playerMovement.circleColl.enabled = false;

        if (VariableManager.isChoking)
            VariableManager.ReleaseChoke();
        if (VariableManager.isDraging)
            VariableManager.ReleaseBody();
    }
    public void GetOutBox()
    {
        Debug.Log("Getting Out box");

        playerMovement.SetPosition(VariableManager.box.exitLocation);
        playerMovement.circleColl.enabled = true;

    }

    IEnumerator ChokeEnemy()
    {
        int qKeyPressCount = 0;

        while (isQPressed && qKeyPressCount < 10)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Q));

            qKeyPressCount++;

            if (qKeyPressCount == 10)
            {
                VariableManager.choke.ChangeState(EnemyStates.EnemyState.Unconscious);
                VariableManager.ReleaseChoke();
                isQPressed = false;
            }
        }
    }
}
