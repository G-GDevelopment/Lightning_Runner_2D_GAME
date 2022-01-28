using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootState : PlayerAblilityState
{
    private int _inputX;
    private bool _playerGrounded;
    public PlayerShootState(Player p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, string p_animboolName) : base(p_player, p_stateMachine, p_playerData, p_animboolName)
    {
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();

        isAbilityDone = true;
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        if (!isExistingState)
        {
            core.Ability.Shoot();

        }

    }


    public override void StandardUpdate()
    {
        base.StandardUpdate();

        _inputX = player.InputHandler.NormalizeInputX;
        _playerGrounded = core.CollisionSenses.IsGrounded;


        core.Movement.ShouldFlip(_inputX);
    }


}
