using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{
    public PlayerState CurrentState { get; private set; }
    public void Initialize(PlayerState p_startingState)
    {
        CurrentState = p_startingState;
        CurrentState.EnterState();
    }

    public void ChangeState(PlayerState p_newState)
    {
        CurrentState.ExitState();
        CurrentState = p_newState;
        CurrentState.EnterState();
    }
}
