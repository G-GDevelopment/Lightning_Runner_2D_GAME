using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPullState : PlayerAblilityState
{
    public PlayerPullState(Player p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, string p_animboolName) : base(p_player, p_stateMachine, p_playerData, p_animboolName)
    {
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();

        isAbilityDone = true;
    }

    public override void ChecksForSwitchingState()
    {
        base.ChecksForSwitchingState();
    }

    public override void EnterState()
    {
        base.EnterState();
        if (core.CollisionSenses.IsTouchingGodRemnants)
        {
            core.Ability.SetIsPullingToTrue();
            core.Ability.RefillGodRemant();

        }
        else
        {
            //isAbilityDone = true;
        }

    }

    public override void ExitState()
    {
        base.ExitState();
        core.Ability.SetIsPullingToFalse();
    }

    public override void StandardUpdate()
    {
        base.StandardUpdate();

        core.Movement.SetVelocityZero();
    }
}
