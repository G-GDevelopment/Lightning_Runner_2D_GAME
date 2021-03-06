using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHyperDashState : PlayerAblilityState
{
    /// <summary>
    /// HyperDash teleports player to the position the bullet collide with something
    /// </summary>
    /// <param name="p_player"></param>
    /// <param name="p_stateMachine"></param>
    /// <param name="p_playerData"></param>
    /// <param name="p_animboolName"></param>
    /// 
    private float _fadeAmount = 1;
    public PlayerHyperDashState(Player p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, string p_animboolName) : base(p_player, p_stateMachine, p_playerData, p_animboolName)
    {
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();

    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        if (!isExistingState)
        {
            core.Ability.HyperDashShoot();
            core.Ability.PlayHyperDashParticles();
        }

    }

    public override void EnterState()
    {
        base.EnterState();

        core.Movement.SetVelocityZero();
    }

    public override void StandardUpdate()
    {
        base.StandardUpdate();


        if (core.Ability.HyperShootHasFoundPosition)
        {
            core.Ability.UseGodRemnant();
            core.Ability.StartHyperDashTeleportShader(core.Ability.FadeControllerDown());


            if(Time.time >= startTime + playerData.HyperDashWaitTime)
            {
                player.transform.position = core.Ability.HyperDashPosition;

                core.Ability.IsTeleporting = true;
                core.Ability.SetFoundPositionToFalse();

                isAbilityDone = true;

            }
        }
        else if (!core.Ability.HyperShootHasFoundPosition && Time.time >= startTime + playerData.HyperDashTime)
        {

            isAbilityDone = true;
        }
    }

}
