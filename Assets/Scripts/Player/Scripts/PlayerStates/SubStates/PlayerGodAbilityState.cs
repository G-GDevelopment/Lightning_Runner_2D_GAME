using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGodAbilityState : PlayerAblilityState
{
    public int CurrentAbility { get; private set; }
    //0 = Pull/Shoot
    //1 = LongDash
    //2 = ShieldDash

    public PlayerGodAbilityState(Player p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, string p_animboolName) : base(p_player, p_stateMachine, p_playerData, p_animboolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        player.InputHandler.SetAbilityInputToFalse();

        if (!isExistingState)
        {
            if (!core.Ability.HasGodRemnant && CurrentAbility != 0)
            {
                isAbilityDone = true;
            }
            else if(CurrentAbility == 0)
            {
                if (core.Ability.HasGodRemnant)
                {
                    stateMachine.ChangeState(player.ShootState);
                    isAbilityDone = true;
                }
                else
                {
                    stateMachine.ChangeState(player.PullState);
                    isAbilityDone = true;
                }
            }
            else if(CurrentAbility == 1 && core.Ability.HasGodRemnant)
            {
                stateMachine.ChangeState(player.HyperDashState);
                isAbilityDone = true;
            }
            else if(CurrentAbility == 2 && core.Ability.HasGodRemnant)
            {
                stateMachine.ChangeState(player.ShieldDashState);
                isAbilityDone = true;
            }

        }

    }

    public void ChangeAbility()
    {
        if(!core.Ability.HasObtainedPullShoot && !core.Ability.HasObtainedHyperDash && !core.Ability.HasObtainedShieldDash)
        {
            CurrentAbility = -1;
            player.InputHandler.SetChangeAbilityInputToFalse();
        }
        else if(core.Ability.HasObtainedPullShoot && !core.Ability.HasObtainedHyperDash && !core.Ability.HasObtainedShieldDash)
        {
            CurrentAbility = 0;
            player.InputHandler.SetChangeAbilityInputToFalse();
        }
        else if(core.Ability.HasObtainedPullShoot && core.Ability.HasObtainedHyperDash && !core.Ability.HasObtainedShieldDash)
        {
            CurrentAbility++;
            if(CurrentAbility >= 2)
            {
                CurrentAbility = 0;
            }
            player.InputHandler.SetChangeAbilityInputToFalse();
        }
        else if(core.Ability.HasObtainedPullShoot && core.Ability.HasObtainedHyperDash && core.Ability.HasObtainedShieldDash)
        {
            CurrentAbility++;
            if(CurrentAbility >= 3)
            {
                CurrentAbility = 0;
            }
            player.InputHandler.SetChangeAbilityInputToFalse();
        }
    }

}
