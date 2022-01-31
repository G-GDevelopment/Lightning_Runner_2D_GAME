using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanguardIdleState : VanguardPassiveState
{
    public VanguardIdleState(Vanguard p_vanguard, VanguardStateMachine p_stateMachine, VanguardData p_data, string p_animBoolName) : base(p_vanguard, p_stateMachine, p_data, p_animBoolName)
    {
    }

    public override void CheckForSwitchingStates()
    {
        base.CheckForSwitchingStates();
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void StandardUpdate()
    {
        base.StandardUpdate();

        if(!isPlayerSeen && isPatrolPath)
        {
            stateMachine.ChangeState(vanguard.PatrolState);
        }
    }
}
