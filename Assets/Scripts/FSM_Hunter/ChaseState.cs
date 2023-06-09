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
        _cat.BuffSpeed(2);
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
            _cat.Move();
            _cat.ObstacleAvoidance();
            _cat.Chase(mouseFound.GetComponent<SteeringAgent>());
            _cat.CheckDestroyDistance();
        }
    }

}
