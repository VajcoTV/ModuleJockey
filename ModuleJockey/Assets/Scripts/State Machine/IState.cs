using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    void EnterState();
    void ExecuteState();
    void ExitState();

}
