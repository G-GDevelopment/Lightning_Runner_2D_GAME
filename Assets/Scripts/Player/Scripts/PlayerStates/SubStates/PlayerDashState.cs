using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAblilityState
{
    public bool CanDash { get; private set; }

    private bool _dashInput;
    private bool _dashInputStop;
    private bool _isHolding;

    private Vector2 _dashDirection;
    private Vector2 _dashDirectionInput;

    private float _lastDashTime;
    public PlayerDashState(Player p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, string p_animboolName) : base(p_player, p_stateMachine, p_playerData, p_animboolName)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        CanDash = false;
        player.InputHandler.SetDashInputToFalse();


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


        core.Movement.ShouldFlip(Mathf.RoundToInt(_dashDirection.x));


        core.Movement.SetVelocity(playerData.DashVelocity, _dashDirection);

        /*
        _isHolding = true;   //Remove This later and save for LongDash State
        Time.timeScale = playerData.HoldTimeScale;  //Remove This later and save for LongDash State
        startTime = Time.unscaledTime;   //Remove This later and save for LongDash State
        */
    }
    public override void ExitState()
    {
        base.ExitState();

        core.Movement.SetVelocity(playerData.DashEndYMultiplier , core.Movement.CurrentVelocity);

        if(core.Movement.CurrentVelocity.y > 0)
        {
            core.Movement.SetVelocityY(core.Movement.CurrentVelocity.y * playerData.DashEndYMultiplier);
        }
    }

    public override void StandardUpdate()
    {
        base.StandardUpdate();

        _dashInput = player.InputHandler.DashInput;
        _dashInputStop = player.InputHandler.DashInputStop;

        player.Animator.SetFloat("YVelocity", core.Movement.CurrentVelocity.y);
        player.Animator.SetFloat("XVelocity",Mathf.Abs(core.Movement.CurrentVelocity.x));
        Debug.Log("Speed" + playerData.DashVelocity);
        Debug.Log(_dashDirection);
        Debug.Log("RB "+ player.Rigidbody.velocity);
        /*
                if (!isExistingState)
                {

                    if(!(Time.time >= startTime + playerData.DashTime))
                    {
                        player.Rigidbody.drag = playerData.Drag;
                        core.Movement.SetVelocity(playerData.DashVelocity, _dashDirection);

                    }
                    else
                    {
                        player.Rigidbody.drag = playerData.PlayerDrag;
                        isAbilityDone = true;
                        _lastDashTime = Time.time;

                    }
                }*/

        if (Time.time >= startTime + playerData.DashTime)
        {
            player.Rigidbody.drag = playerData.PlayerDrag;
            _lastDashTime = Time.time;
            isAbilityDone = true;
        }
    }

    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= _lastDashTime + playerData.DashCooldown;
    }

    public void ResetCanDash() => CanDash = true;

    private void StartLongDashMode()
    {
        ////////////
        /// Long Dash Mode instead
        ///

        if (_isHolding) //This Should be DashInput instead - So when player click Dash then dash 
        {
            _dashDirectionInput = player.InputHandler.DashDirectionInput;

            if (_dashDirectionInput != Vector2.zero)
            {
                _dashDirection = _dashDirectionInput;
                _dashDirection.Normalize();
            }

            float angle = Vector2.SignedAngle(Vector2.right, _dashDirection);

            if (_dashInputStop || Time.unscaledTime >= startTime + playerData.MaxHoldtime)
            {
                _isHolding = false;
                Time.timeScale = 1f;
                startTime = Time.time;
                core.Movement.ShouldFlip(Mathf.RoundToInt(_dashDirection.x));
                player.Rigidbody.drag = playerData.Drag;

                core.Movement.SetVelocity(playerData.DashVelocity, _dashDirection);
            }
        }
        else
        {
            core.Movement.SetVelocity(playerData.DashVelocity, _dashDirection);

            if (Time.time >= startTime + playerData.DashTime)
            {
                player.Rigidbody.drag = playerData.PlayerDrag;
                isAbilityDone = true;
                _lastDashTime = Time.time;
            }
        }
    }

}
