using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{

    Cat _cat;

    public ChaseState(Cat c)
    {
        _cat = c;
    }

    public override void OnEnter()
    {
        Debug.Log("!!!! mouse");
    }

    public override void OnExit()
    {

    }

    public override void Update()
    {

        var mouse = _cat.FOV.FieldOfViewCheck();

        if (!mouse)
        {
            _cat.Chase(mouse);
        }
        else
        {
            fsm.ChangeState(States.Patrol);
        }
    }

}
