using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanguardActiveState : VanguardState
{
    protected bool isDoneChasing;

    protected bool isPatrolPath;
    protected bool isPlayerSeen;
    public VanguardActiveState(Vanguard p_vanguard, VanguardStateMachine p_stateMachine, VanguardData p_data, string p_animBoolName) : base(p_vanguard, p_stateMachine, p_data, p_animBoolName)
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


        isDoneChasing = false;
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

        if (!isExistingState)
        {
            if (isDoneChasing)
            {
                if (!isPlayerSeen && !isPatrolPath)
                {
                    stateMachine.ChangeState(vanguard.IdleState);
                }
                else if (!isPlayerSeen && isPatrolPath)
                {
                    stateMachine.ChangeState(vanguard.PatrolState);
                }
            }
        }
    }
}
