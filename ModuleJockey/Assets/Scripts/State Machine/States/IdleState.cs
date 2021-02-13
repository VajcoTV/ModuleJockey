using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public void EnterState()
    {
        Debug.Log("Entering Idle");
    }

    public void ExecuteState()
    {
        Debug.Log("executing idle");
    }

    public void ExitState()
    {
        Debug.Log("Exiting idle");
    }
}
