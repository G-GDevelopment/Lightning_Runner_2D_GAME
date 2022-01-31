using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanguardPassiveState : VanguardState
{
    protected bool isPatrolPath;
    protected bool isPlayerSeen;

    public VanguardPassiveState(Vanguard p_vanguard, VanguardStateMachine p_stateMachine, VanguardData p_data, string p_animBoolName) : base(p_vanguard, p_stateMachine, p_data, p_animBoolName)
    {
    }

    public override void CheckForSwitchingStates()
    {
        base.CheckForSwitchingStates();

        isPatrolPath = core.VanguardAbility.IsPatroling;
        isPlayerSeen = core.CollisionSystem.IsPlayerSeen;
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
        if (isPlayerSeen)
        {
            Debug.Log("Player is Seen");
            stateMachine.ChangeState(vanguard.FollowState);
        }

    }
}
