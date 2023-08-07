using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{

    Cat_NavMesh _cat;

    public ChaseState(Cat_NavMesh c)
    {
        _cat = c;
    }

    public override void OnEnter()
    {
        AudioManager.Instance.PlayMeowSound();
        Debug.Log("!!!! mouse");
        _cat.BuffSpeed(1.35f);
    }

    public override void OnExit()
    {

    }

    public override void Update()
    {

        var mouseFound = _cat.FOV.FieldOfViewCheck();


        if (mouseFound == null || _cat.mouse.IsInWallHole)
        {
            fsm.ChangeState(States.Patrol);
        }
        else
        {
            if (mouseFound.CompareTag("Decoy")){
                _cat.ChaseMouse(mouseFound);//pasar mouse por aca?
            }
            else
            {
                _cat.CheckDestroyDistance();
            }

            
        }
    }

}
