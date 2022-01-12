using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAblilityState : PlayerState
{
    protected bool isAbilityDone;

    private bool _isGrounded; //Set to protected if more states need this
    public PlayerAblilityState(Player p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, string p_animboolName) : base(p_player, p_stateMachine, p_playerData, p_animboolName)
    {
    }

    public override void ChecksForSwitchingState()
    {
        base.ChecksForSwitchingState();

        _isGrounded = core.CollisionSenses.IsGrounded;
    }

    public override void EnterState()
    {
        base.EnterState();

        isAbilityDone = false;
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
            if (isAbilityDone)
            {
                if (_isGrounded && core.Movement.CurrentVelocity.y < 0.01f)
                {
                    stateMachine.ChangeState(player.IdleState);
                }
                else
                {
                    stateMachine.ChangeState(player.InAirState);
                }
            }

        }
    }
}
