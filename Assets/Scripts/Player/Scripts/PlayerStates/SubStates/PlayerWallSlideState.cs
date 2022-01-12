using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, string p_animboolName) : base(p_player, p_stateMachine, p_playerData, p_animboolName)
    {
    }

    public override void StandardUpdate()
    {
        base.StandardUpdate();
        if (!isExistingState)
        {
            core.Movement.SetVelocityY(-playerData.WallSlideVelocity);

            if(grabInput && inputY == 0 && !isExistingState && playerData.ShouldPlayerBeAbleToClimb)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }


        }
    }
}
