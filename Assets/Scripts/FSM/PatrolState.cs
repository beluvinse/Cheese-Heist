using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{

    private Cat_NavMesh _cat;

    public PatrolState(Cat_NavMesh c)
    {
        _cat = c;
    }

    public override void OnEnter()
    {
        Debug.Log("Momento:Waypoints");
        _cat.SetBaseSpeed();
        _cat.WaypointSystem(true);
    }

    public override void OnExit()
    {
        
    }

    public override void Update()
    {
        var mouseFound = _cat.FOV.FieldOfViewCheck();

        if (!_cat.mouse.IsRooted )
        {
            if (mouseFound != null && !_cat.mouse.IsInWallHole)
            {
                fsm.ChangeState(States.Chase);
            }
            else
            {
                _cat.WaypointSystem(false);
            }
        }
        //else
        //{
        //    fsm.ChangeState(States.MouseTrapped);
        //}
            

    }


}
