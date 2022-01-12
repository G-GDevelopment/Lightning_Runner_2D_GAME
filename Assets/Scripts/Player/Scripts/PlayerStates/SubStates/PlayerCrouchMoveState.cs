using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(Player p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, string p_animboolName) : base(p_player, p_stateMachine, p_playerData, p_animboolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

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
            core.Movement.SetVelocityX(playerData.CrouchMovementVelocity * core.Movement.FacingDirection);
            core.Movement.ShouldFlip(inputX);

            if(inputX == 0)
            {
                stateMachine.ChangeState(player.CrouchIdleState);
            }
            else if(inputY != -1 && !isTouchingCelling)
            {
                stateMachine.ChangeState(player.MovementState);
            }
        }
    }
}
