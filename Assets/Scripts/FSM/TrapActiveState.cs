using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActiveState : State
{
    Cat _cat;


    public TrapActiveState(Cat c)
    {
        _cat = c;
    }

    public override void OnEnter()
    {
        Debug.Log("mouse trapped");
    }

    public override void OnExit()
    {

    }


    public override void Update()
    {
        _cat.Move();
        _cat.Chase(_cat.mouse.GetComponent<SteeringAgent>());

        var mouseFound = _cat.FOV.FieldOfViewCheck();


        if (mouseFound != null)
        {
            fsm.ChangeState(States.Chase);
        }
    }
}
