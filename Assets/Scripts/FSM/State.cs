using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public FiniteStateMachine fsm; //comodo pero trae problemas con genericos (tener la referencia en los estados mismos)
    public abstract void Update();
    public abstract void OnEnter();
    public abstract void OnExit();

    //Agregar FixedUpdate si se lo necesita
}
