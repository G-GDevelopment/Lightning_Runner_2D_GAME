using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 _holdPosition;
    public PlayerWallGrabState(Player p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, string p_animboolName) : base(p_player, p_stateMachine, p_playerData, p_animboolName)
    {
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void ChecksForSwitchingState()
    {
        base.ChecksForSwitchingState();
    }

    public override void EnterState()
    {
        base.EnterState();

        _holdPosition = player.transform.position;

        HoldPosition();
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
            HoldPosition();
            if (inputY > 0 && playerData.ShouldPlayerBeAbleToClimb)
            {
                stateMachine.ChangeState(player.WallClimbState);

            }else if(inputY < 0 || !grabInput)
            {
                stateMachine.ChangeState(player.WallSlideSlide);

            }

        }
    }

    private void HoldPosition()
    {
        player.transform.position = _holdPosition;
        core.Movement.SetVelocityX(0f);
        core.Movement.SetVelocityY(0f);

    }
}
