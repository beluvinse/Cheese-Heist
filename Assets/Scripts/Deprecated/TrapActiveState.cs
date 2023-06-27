using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActiveState : State
{
    Cat_NavMesh _cat;

    public static event Action OnTrapActive;


    public TrapActiveState(Cat_NavMesh c)
    {
        _cat = c;
    }

    public override void OnEnter()
    {
        Debug.Log("mouse trapped");
        OnTrapActive?.Invoke();
    }

    public override void OnExit()
    {

    }


    public override void Update()
    {
        //_cat.Move();
        //_cat.Chase(_cat.mouse.GetComponent<SteeringAgent>());

        var mouseFound = _cat.FOV.FieldOfViewCheck();


        if (mouseFound != null)
        {
            fsm.ChangeState(States.Chase);
        }
    }
}
