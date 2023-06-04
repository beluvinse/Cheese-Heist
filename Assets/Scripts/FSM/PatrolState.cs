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
    }

    public override void OnExit()
    {
        
    }

    public override void Update()
    {
        //var mouseFound = _cat.FOV.FieldOfViewCheck();

        //if (!_cat.mouse.isRooted )
        //{
        //    if (mouseFound != null)
        //    {
        //        fsm.ChangeState(States.Chase);
        //    }
        //    else
        //    {
        Debug.Log("??????");
        _cat.WaypointSystem();


        //    }
        //}
        //else
        //{
        //    fsm.ChangeState(States.MouseTrapped);
        //}
            

    }


}
