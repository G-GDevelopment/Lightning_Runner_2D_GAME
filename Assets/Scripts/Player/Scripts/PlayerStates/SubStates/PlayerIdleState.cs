using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, string p_animboolName) : base(p_player, p_stateMachine, p_playerData, p_animboolName)
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
        if (!isExistingState)
        {
            if (inputX != 0)
            {
                stateMachine.ChangeState(player.MovementState);
            }
            else if(inputY == -1)
            {
                stateMachine.ChangeState(player.CrouchIdleState);
            }

        }
    }
}
