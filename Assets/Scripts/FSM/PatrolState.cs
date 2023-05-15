using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{

    private Cat _cat;

    public PatrolState(Cat c)
    {
        _cat = c;
    }

    public override void OnEnter()
    {
        Debug.Log("Momento:Waypoints");
        _cat.BaseSpeed();
    }

    public override void OnExit()
    {
        
    }

    public override void Update()
    {
        var mouseFound = _cat.FOV.FieldOfViewCheck();

        if (!_cat.mouse.isRooted )
        {
            if (mouseFound != null)
            {
                fsm.ChangeState(States.Chase);
            }
            else
            {
                _cat.Move();
                _cat.FollowWaypoints();
                //_cat.ObstacleAvoidance();

            }
        }
        else
        {
            fsm.ChangeState(States.MouseTrapped);
        }
            

    }


}
