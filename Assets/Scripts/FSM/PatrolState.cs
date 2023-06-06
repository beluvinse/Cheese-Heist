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
                _cat.WaypointSystem();
            }
        }
        //else
        //{
        //    fsm.ChangeState(States.MouseTrapped);
        //}
            

    }


}
