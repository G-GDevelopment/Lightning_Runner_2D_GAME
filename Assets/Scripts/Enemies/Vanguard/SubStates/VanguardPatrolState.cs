using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanguardPatrolState : VanguardPassiveState
{
    private int _direction;
    public VanguardPatrolState(Vanguard p_vanguard, VanguardStateMachine p_stateMachine, VanguardData p_data, string p_animBoolName) : base(p_vanguard, p_stateMachine, p_data, p_animBoolName)
    {
    }

    public override void CheckForSwitchingStates()
    {
        base.CheckForSwitchingStates();
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("Start Patrol");
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

        if(core.CollisionSystem.WallFront || (!core.CollisionSystem.IsOnEdge && core.CollisionSystem.IsGrounded))  //|| (core.CollisionSystem.CloseToPatrolPoint)
        {
            core.VanguardMovement.Flip();
        }
        else
        {
            core.VanguardMovement.SetVelocityX(core.VanguardMovement.FacingDirection * vanguardData.MovementSpeed);
        }



        if(!isPlayerSeen && !isPatrolPath)
        {
            stateMachine.ChangeState(vanguard.IdleState);
        }
    }


}
