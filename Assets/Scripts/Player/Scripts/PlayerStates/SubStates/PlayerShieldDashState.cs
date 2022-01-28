using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldDashState : PlayerAblilityState
{
    private bool _hasTouchedWater;
    private Vector2 _dashDirection;
    private Vector2 _dashDirectionInput;
    private Vector2 _saveDirection;
    private int _inputX;

    public PlayerShieldDashState(Player p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, string p_animboolName) : base(p_player, p_stateMachine, p_playerData, p_animboolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        core.Ability.UseGodRemnant();
        player.DashState.UseCanDash();
        _hasTouchedWater = false;
        player.InputHandler.SetDashInputToFalse();
        core.Ability.SetShieldDashingToTrue();

        _dashDirectionInput = player.InputHandler.DashDirectionInput;

        if (_dashDirectionInput != Vector2.zero)
        {
            _dashDirection = _dashDirectionInput;
            _dashDirection.Normalize();

        }
        else
        {
            _dashDirection = Vector2.right * core.Movement.FacingDirection;
        }

        _saveDirection = _dashDirection;
        core.Movement.ShouldFlip(Mathf.RoundToInt(_dashDirection.x));
    }

    public override void ExitState()
    {
        base.ExitState();
        core.Ability.SetShieldDashingToFalse();
    }

    public override void StandardUpdate()
    {
        base.StandardUpdate();

        _inputX = player.InputHandler.NormalizeInputX;
        player.Rigidbody.drag = playerData.Drag;


        WaterDash();

        core.Movement.SetVelocityX(playerData.ShieldDashVelocity * _saveDirection.x);

        if (Time.time >= startTime + playerData.ShieldDashTime && !_hasTouchedWater || (_hasTouchedWater && !core.CollisionSenses.IsTouchingWater))
        {
            player.Rigidbody.drag = playerData.PlayerDrag;
            core.Movement.SetVelocityZero();
            isAbilityDone = true;
        }
    }

    private void WaterDash()
    {
        if (core.CollisionSenses.IsTouchingWater)
        {
            core.Movement.SetVelocity(playerData.DashVelocity, _dashDirection);
            _hasTouchedWater = true;
        }
    }
}
