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
        Debug.Log("!!!! mouse");
        _cat.BuffSpeed(1.5f);
    }

    public override void OnExit()
    {

    }

    public override void Update()
    {

        var mouseFound = _cat.FOV.FieldOfViewCheck();


        if (mouseFound == null)
        {
            fsm.ChangeState(States.Patrol);
        }
        else
        {
            _cat.ChaseMouse();//pasar mouse por aca?
        }
    }

}
