using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, string p_animboolName) : base(p_player, p_stateMachine, p_playerData, p_animboolName)
    {
    }

    public override void StandardUpdate()
    {
        base.StandardUpdate();
        if (!isExistingState)
        {
            if(inputX != 0)
            {

                stateMachine.ChangeState(player.MovementState);

            }
            else if (isAnimationFinished)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }
}
