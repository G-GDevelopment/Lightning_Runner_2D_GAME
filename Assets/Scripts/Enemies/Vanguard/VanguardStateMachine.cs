using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanguardStateMachine
{
    public VanguardState CurrentState { get; private set; }
    public void Initialize(VanguardState p_startingState)
    {
        CurrentState = p_startingState;
        CurrentState.EnterState();
    }

    public void ChangeState(VanguardState p_newState)
    {
        CurrentState.ExitState();
        CurrentState = p_newState;
        CurrentState.EnterState();
    }
}
