using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponents
{
    public Rigidbody2D Rigidbody;
    public int FacingDirection { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }

    private Vector2 _playerVelocity;

    #region Built In Methods

    protected override void Awake()
    {
        base.Awake();

        Rigidbody = GetComponentInParent<Rigidbody2D>();
        //Facing Right from Start
        FacingDirection = 1;
    }

    public void LogicUpdate()
    {
        CurrentVelocity = Rigidbody.velocity;
    }
    #endregion

    #region Set Methods
    public void SetVelocityZero()
    {
        Rigidbody.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }
    public void SetVelocity(float p_velocity, Vector2 p_angle, int p_direction)
    {
        p_angle.Normalize();
        _playerVelocity.Set(p_angle.x * p_velocity * p_direction, p_angle.y * p_velocity);
        Rigidbody.velocity = _playerVelocity;
        CurrentVelocity = _playerVelocity;
    }

    public void SetVelocity(float p_velocity, Vector2 p_direction)
    {
        _playerVelocity = p_direction * p_velocity;
        Rigidbody.velocity = _playerVelocity;
        CurrentVelocity = _playerVelocity;
    }
    public void SetVelocityX(float p_velocity)
    {
        _playerVelocity.Set(p_velocity, CurrentVelocity.y);
        Rigidbody.velocity = _playerVelocity;
        CurrentVelocity = _playerVelocity;
    }

    public void SetVelocityY(float p_velocity)
    {
        _playerVelocity.Set(CurrentVelocity.x, p_velocity);
        Rigidbody.velocity = _playerVelocity;
        CurrentVelocity = _playerVelocity;
    }

    public void SmoothFalling(float p_fallMulitplier, float p_lowJumpMultiplier, bool p_jumpInput)
    {
        if (Rigidbody.velocity.y < 0)
        {
            _playerVelocity += Vector2.up * Physics2D.gravity.y * (p_fallMulitplier - 1) * Time.deltaTime;
            Rigidbody.velocity = _playerVelocity;
            CurrentVelocity = _playerVelocity;
        }
        else if (Rigidbody.velocity.y > 0 && !p_jumpInput) //!InputHandler.JumpInput
        {
            _playerVelocity += Vector2.up * Physics2D.gravity.y * (p_lowJumpMultiplier - 1) * Time.deltaTime;
            Rigidbody.velocity = _playerVelocity;
            CurrentVelocity = _playerVelocity;
        }
    }

    public void ShouldFlip(int p_inputX)
    {
        if (p_inputX != 0 && p_inputX != FacingDirection)
        {
            Flip();
        }
    }

    private void Flip()
    {
        FacingDirection *= -1;
        Rigidbody.transform.Rotate(0.0f, 180.0f, 0.0f);

    }

    #endregion
}
