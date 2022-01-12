using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAblilityState
{
    private int _wallJumpDirection;
    public PlayerWallJumpState(Player p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, string p_animboolName) : base(p_player, p_stateMachine, p_playerData, p_animboolName)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        player.InputHandler.SetJumpInputToFalse();
        player.JumpState.ResetAmountOfJumpsLeft();
        core.Movement.SetVelocity(playerData.WallJumpVelocity, playerData.WallJumpAngle, _wallJumpDirection);
        core.Movement.ShouldFlip(_wallJumpDirection);
        player.JumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void StandardUpdate()
    {
        base.StandardUpdate();

        player.Animator.SetFloat("YVelocity", core.Movement.CurrentVelocity.y);
        player.Animator.SetFloat("XVelocity", Mathf.Abs(core.Movement.CurrentVelocity.x));

        if(Time.time >= startTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool p_isTouchingWall)
    {
        if (p_isTouchingWall)
        {
            _wallJumpDirection = -core.Movement.FacingDirection;
        }
        else
        {
            _wallJumpDirection = core.Movement.FacingDirection;
        }
    }
}
