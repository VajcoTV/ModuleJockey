using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private IState currentState;

    public void ChangeState(IState newState)
    {
        if(currentState != null)
        {
            currentState.ExitState();
        }
        currentState = newState;
        currentState.EnterState();

    }

}

