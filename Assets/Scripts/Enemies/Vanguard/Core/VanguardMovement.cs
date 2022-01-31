using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanguardMovement : EnemyCoreComponents{
    public Rigidbody2D Rigidbody;
    public int FacingDirection { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }

    private Vector2 _enemyVelocity;


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
    public void SetVelocityZero()
    {
        Rigidbody.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }
    public void SetVelocityX(float p_velocity)
    {
        _enemyVelocity.Set(p_velocity, CurrentVelocity.y);
        Rigidbody.velocity = _enemyVelocity;
        CurrentVelocity = _enemyVelocity;
    }
    public void ShouldVelocityFlip()
    {
        if (CurrentVelocity.x != 0 && CurrentVelocity.x != FacingDirection)
        {
            Flip();
        }
    }

    public void Flip()
    {
        FacingDirection *= -1;

        Rigidbody.transform.Rotate(0.0f, 180.0f, 0.0f);

    }
}
