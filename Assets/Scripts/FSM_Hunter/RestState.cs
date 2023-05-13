using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestState : State
{

    Hunter _hunter;
    float _restore;

    public RestState(Hunter h, float restore)
    {
        _hunter = h;
        _restore = restore;
    }

    public override void OnEnter()
    {
        _hunter.Rest();
    }

    public override void OnExit()
    {
    }

    public override void Update()
    {
        _hunter.EnergyRegen(_restore);
        if(_hunter.FullEnergy)
            fsm.ChangeState(HunterStates.Patrol);

    }
}
