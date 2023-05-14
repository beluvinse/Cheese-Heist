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
        _cat.Move();
        _cat.FollowWaypoints();
        //_cat.ObstacleAvoidance();
    }


}
