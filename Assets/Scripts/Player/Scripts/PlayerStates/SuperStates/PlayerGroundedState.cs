using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int inputX;
    protected int inputY;
    protected bool isTouchingCelling;

    private bool _jumpInput;
    private bool _grabInput;
    private bool _isGrounded;
    private bool _isTouchingWall;
    private bool _isTouchingLedge;
    private bool _dashInput;
    private bool _changeAbilityInput;
    private bool _abilityInput;

    public PlayerGroundedState(Player p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, string p_animboolName) : base(p_player, p_stateMachine, p_playerData, p_animboolName)
    {
    }

    public override void ChecksForSwitchingState()
    {
        base.ChecksForSwitchingState();

        _isGrounded = core.CollisionSenses.IsGrounded;
        _isTouchingWall = core.CollisionSenses.WallFront;
        _isTouchingLedge = core.CollisionSenses.IsTouchingLedge;
        isTouchingCelling = core.CollisionSenses.UnderCelling;
    }

    public override void EnterState()
    {
        base.EnterState();

        player.JumpState.ResetAmountOfJumpsLeft();
        player.DashState.ResetCanDash();

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

        inputX = player.InputHandler.NormalizeInputX;
        inputY = player.InputHandler.NormalizeInputY;
        _jumpInput = player.InputHandler.JumpInput;
        _grabInput = player.InputHandler.GrabInput;
        _dashInput = player.InputHandler.DashInput;
        _changeAbilityInput = player.InputHandler.AbilityChangeInput;
        _abilityInput = player.InputHandler.AbilityInput;

        if (core.Ability.IsTeleporting)
        {
            core.Ability.StartHyperDashTeleportShader(core.Ability.FadeControllerUp());
        }

        core.Ability.UpdateText(player.GodAbilityState.CurrentAbility); // Prototyping

        if (player.InputHandler.AbilityInput && !isTouchingCelling && core.Ability.HasObtainedPullShoot)
        {
            stateMachine.ChangeState(player.GodAbilityState);
        }else if (player.InputHandler.AbilityChangeInput && !isTouchingCelling && core.Ability.HasObtainedPullShoot)
        {
            player.GodAbilityState.ChangeAbility();
            Debug.Log(player.GodAbilityState.CurrentAbility);
        }
        else if (_jumpInput && player.JumpState.CanJump() && !isTouchingCelling)
        {

            stateMachine.ChangeState(player.JumpState);
        }
        else if (!_isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
        else if(_isTouchingWall && _grabInput && _isTouchingLedge)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if (_dashInput && player.DashState.CheckIfCanDash() && inputX == 0 && !isTouchingCelling)
        {
            stateMachine.ChangeState(player.DashState);
        }

    }
}
