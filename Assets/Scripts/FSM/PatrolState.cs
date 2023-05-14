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
    }

    public override void OnExit()
    {
        
    }

    public override void Update()
    {
        var mouse = _cat.FOV.FieldOfViewCheck();
        if (mouse != null)
        {
            Debug.Log("ss");
            fsm.ChangeState(States.Chase);
        }
        else
        {
            _cat.Move();
            _cat.FollowWaypoints();
            //_cat.ObstacleAvoidance();

        }

    }


}
