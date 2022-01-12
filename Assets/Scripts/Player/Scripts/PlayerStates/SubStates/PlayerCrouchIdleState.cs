using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(Player p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, string p_animboolName) : base(p_player, p_stateMachine, p_playerData, p_animboolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        core.Movement.SetVelocityZero();
        player.SetColliderHeight(playerData.CrouchColliderHeight);
    }

    public override void ExitState()
    {
        base.ExitState();

        player.SetColliderHeight(playerData.ColliderHeightStandard);
    }

    public override void StandardUpdate()
    {
        base.StandardUpdate();

        if (!isExistingState)
        {
            if(inputX != 0)
            {
                stateMachine.ChangeState(player.CrouchMoveState);
            }
            else if(inputY != -1 && !isTouchingCelling)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }
}
