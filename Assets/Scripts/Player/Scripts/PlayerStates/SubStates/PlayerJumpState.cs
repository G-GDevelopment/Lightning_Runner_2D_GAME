using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAblilityState
{
    private int _amountOfJumpsLeft;
    public PlayerJumpState(Player p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, string p_animboolName) : base(p_player, p_stateMachine, p_playerData, p_animboolName)
    {
        _amountOfJumpsLeft = playerData.AmountOfJumps;
    }

    public override void EnterState()
    {
        base.EnterState();
        player.InputHandler.SetJumpInputToFalse();

        core.Movement.SetVelocityY(playerData.JumpForce);

        isAbilityDone = true;
        _amountOfJumpsLeft--;
        player.InAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        if(_amountOfJumpsLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = playerData.AmountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => _amountOfJumpsLeft--;
}
