using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 _detectedPos;
    private Vector2 _cornerPos;
    private Vector2 _playerVelocity;
    private Vector2 _startPosition;
    private Vector2 _stopPosition;

    private bool _isHanging;
    private bool _isClimbing;
    private bool _jumpInput;
    private bool _isTouchingCelling;

    private int _inputX;
    private int _inputY;
    public PlayerLedgeClimbState(Player p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, string p_animboolName) : base(p_player, p_stateMachine, p_playerData, p_animboolName)
    {
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();

        player.Animator.SetBool("ClimbLedge", false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        _isHanging = true;
    }

    public override void EnterState()
    {
        base.EnterState();

        core.Movement.SetVelocityZero();

        player.transform.position = _detectedPos;
        _cornerPos = DetermineCornerPosition();

        _startPosition.Set(_cornerPos.x - (core.Movement.FacingDirection * playerData.StartOffset.x), _cornerPos.y - playerData.StartOffset.y);
        _stopPosition.Set(_cornerPos.x + (core.Movement.FacingDirection * playerData.StopOffset.x), _cornerPos.y + playerData.StopOffset.y);

        player.transform.position = _startPosition;

    }

    public override void ExitState()
    {
        base.ExitState();

        _isHanging = false;

        if (_isClimbing)
        {
            player.transform.position = _stopPosition;
            _isClimbing = false;
        }
    }
    public override void StandardUpdate()
    {
        base.StandardUpdate();

        _jumpInput = player.InputHandler.JumpInput;

        if (isAnimationFinished)
        {
            if (_isTouchingCelling)
            {
                stateMachine.ChangeState(player.CrouchIdleState);
            }
            else
            {
                stateMachine.ChangeState(player.IdleState);

            }
        }
        else
        {
            _inputX = player.InputHandler.NormalizeInputX;
            _inputY = player.InputHandler.NormalizeInputY;

            core.Movement.SetVelocityZero();
            player.transform.position = _startPosition;

            if(_inputX == core.Movement.FacingDirection && _isHanging && !_isClimbing ||(_jumpInput && _isHanging && !_isClimbing))
            {
                CheckForSpace();
                _isClimbing = true;
                player.Animator.SetBool("ClimbLedge", true);

            }
            else if(_inputY == -1 && _isHanging && !_isClimbing)
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }

    }

    public void SetDetectedPosition(Vector2 p_position) => _detectedPos = p_position;

    private void CheckForSpace()
    {
        _isTouchingCelling = Physics2D.Raycast(_cornerPos + (Vector2.up * 0.015f) + (Vector2.right * core.Movement.FacingDirection * 0.015f), Vector2.up, playerData.ColliderHeightStandard + 0.2f, core.CollisionSenses.GroundLayer);
        player.Animator.SetBool("IsTouchingCelling", _isTouchingCelling);
    }

    private Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(core.CollisionSenses.WallCheck.position, Vector2.right * core.Movement.FacingDirection, core.CollisionSenses.WallCheckDistance, core.CollisionSenses.GroundLayer);
        float xDist = xHit.distance;
        _playerVelocity.Set((xDist + 0.015f) * core.Movement.FacingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(core.CollisionSenses.LedgeCheck.position + (Vector3)(_playerVelocity), Vector3.down, core.CollisionSenses.LedgeCheck.position.y - core.CollisionSenses.WallCheck.position.y + 0.015f, core.CollisionSenses.GroundLayer);
        float yDist = yHit.distance;

        _playerVelocity.Set(core.CollisionSenses.WallCheck.position.x + (xDist * core.Movement.FacingDirection), core.CollisionSenses.LedgeCheck.position.y - yDist);

        return _playerVelocity;
    }

}
