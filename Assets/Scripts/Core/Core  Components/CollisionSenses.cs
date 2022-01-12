using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponents
{
    #region Variables for checking
    public Transform GroundCheck { get => _groundChecker; private set => _groundChecker = value; }
    public Transform WallCheck { get => _wallChecker; private set => _wallChecker = value; }
    public Transform LedgeCheck { get => _ledgeChecker; private set => _ledgeChecker = value; }
    public Transform CellingCheck { get => _cellingChecker; private set => _cellingChecker = value; }

    public float GroundCheckRadius { get => _groundCheckRadius; set => _groundCheckRadius = value; }
    public float CellingCheckRadius { get => _cellingCheckRadius; set => _cellingCheckRadius = value; }
    public float WallCheckDistance { get => _wallCheckDistance; set => _wallCheckDistance = value; }
    public LayerMask GroundLayer { get => _groundLayer; set => _groundLayer = value; }


    [SerializeField] private Transform _groundChecker;
    [SerializeField] private Transform _wallChecker;
    [SerializeField] private Transform _ledgeChecker;
    [SerializeField] private Transform _cellingChecker;

    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private float _cellingCheckRadius;
    [SerializeField] private float _wallCheckDistance;

    [SerializeField] private LayerMask _groundLayer;


    #endregion



    #region Methods Checking player condition
    public bool UnderCelling
    {
        get => Physics2D.OverlapCircle(_cellingChecker.position, _cellingCheckRadius, _groundLayer);
    }
    public bool IsGrounded
    {
        get => Physics2D.OverlapCircle(_groundChecker.position, _groundCheckRadius, _groundLayer);

    }

    public bool WallFront
    {
        get => Physics2D.Raycast(core.CollisionSenses.WallCheck.position, Vector2.right * core.Movement.FacingDirection, _wallCheckDistance, _groundLayer);
    }

    public bool WallBack
    {
        get => Physics2D.Raycast(core.CollisionSenses.WallCheck.position, Vector2.right * -core.Movement.FacingDirection, _wallCheckDistance, _groundLayer);
    }

    public bool IsTouchingLedge
    {
        get => Physics2D.Raycast(_ledgeChecker.position, Vector2.right * core.Movement.FacingDirection, _wallCheckDistance, _groundLayer);
    }


    #endregion
}
