using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanguardCollisionSystem : EnemyCoreComponents
{
    public Transform GroundChecker { get => _groundChecker; set => _groundChecker = value; }
    public Transform EdgeChecker { get => _edgeChecker; set => _edgeChecker = value; }
    public Transform WallChecker { get => _wallChecker; set => _wallChecker = value; }
    public float GroundCheckRadius { get => _groundCheckRadius; set => _groundCheckRadius = value; }
    public float EdgeCheckDistance { get => _edgeCheckDistance; set => _edgeCheckDistance = value; }
    public float WallCheckDistance { get => _wallCheckDistance; set => _wallCheckDistance = value; }
    public int CurrentPatrolPoint { get => _currentPatrolPoint; set => _currentPatrolPoint = value; }
    public Vector2 CollisionSize { get => _collisionSize; set => _collisionSize = value; }
    public Vector2 OffsetBox { get => _offsetBox; set => _offsetBox = value; }

    [SerializeField] private Transform _groundChecker;
    [SerializeField] private Transform _edgeChecker;
    [SerializeField] private Transform _wallChecker;
    [SerializeField] private Transform[] _patrolPoints;

    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private float _edgeCheckDistance;
    [SerializeField] private float _wallCheckDistance;

    [SerializeField] private int _currentPatrolPoint;

    [SerializeField] private Vector2 _collisionSize;
    [SerializeField] private Vector2 _offsetBox;

    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _playerLayer;





    public bool IsGrounded
    {
        get => Physics2D.OverlapCircle(_groundChecker.position, _groundCheckRadius, _groundLayer);

    }
    public bool IsOnEdge
    {
        get => Physics2D.Raycast(_edgeChecker.position, Vector2.down, _edgeCheckDistance, _groundLayer);

    }
    public bool WallFront
    {
        get => Physics2D.Raycast(_wallChecker.position, Vector2.right * enemyCore.VanguardMovement.FacingDirection, _wallCheckDistance, _groundLayer);
    }

    public bool IsPlayerSeen
    {
        get => Physics2D.OverlapBox(transform.position + (Vector3)_offsetBox, _collisionSize, 0f, _playerLayer);
    }

    public bool CloseToPatrolPoint
    {
        get => Vector2.Distance(transform.position, _patrolPoints[_currentPatrolPoint].position) < 0.2f;
    }
}
