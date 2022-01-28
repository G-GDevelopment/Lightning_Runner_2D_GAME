using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : PlayerGroundedState
{
    private bool _dashInput;
    public PlayerMovementState(Player p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, string p_animboolName) : base(p_player, p_stateMachine, p_playerData, p_animboolName)
    {
    }

    public override void ChecksForSwitchingState()
    {
        base.ChecksForSwitchingState();
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
        _dashInput = player.InputHandler.DashInput;

        core.Movement.ShouldFlip(inputX);

        core.Movement.SetVelocityX(playerData.MovementVelocity * inputX);
        if (!isExistingState)
        {
            if(inputX == 0)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else if(inputY == -1)
            {
                stateMachine.ChangeState(player.CrouchMoveState);
            }
            else if (_dashInput && player.DashState.CheckIfCanDash())
            {
                stateMachine.ChangeState(player.DashState);
            }

        }

    }
}
