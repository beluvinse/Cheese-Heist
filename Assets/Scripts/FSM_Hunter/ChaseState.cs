using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{

    Hunter _hunter;
    float _cost;

    public ChaseState(Hunter h, float cost)
    {
        _hunter = h;
        _cost = cost;
    }

    public override void OnEnter()
    {
        _hunter.sprite.color = Color.red;
    }

    public override void OnExit()
    {

    }

    public override void Update()
    {

        if (_hunter.CheckPursuit() && _hunter.energy > 0 )
        {
            _hunter.Chase(_hunter.CheckPursuit());
            _hunter.EnergyDrain(_cost);
            _hunter.DestroyBoid();
        }
        else if (_hunter.CheckPursuit() == null)
        {

            //fsm.ChangeState(HunterStates.Patrol);
        }
        else
        {

           // fsm.ChangeState(HunterStates.Rest);
        }
    }

}
