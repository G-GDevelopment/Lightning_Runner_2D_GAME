using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables Concerning States
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMovementState MovementState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallSlideState WallSlideSlide { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }
    public PlayerGodAbilityState GodAbilityState { get; private set; }
    public PlayerShootState ShootState { get; private set; }
    public PlayerPullState PullState { get; private set; }
    public PlayerHyperDashState HyperDashState { get; private set; }
    public PlayerShieldDashState ShieldDashState { get; private set; }

    [SerializeField]
    private PlayerData _playerData;
    #endregion

    #region Components
    public Core Core { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public CapsuleCollider2D CapsuleCollider { get; private set; }

    #endregion

    #region Variables
    private Vector2 _playerVelocity;

    [SerializeField] private bool _debugGizmos = false;
    #endregion

    #region Built In Method
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, _playerData, "Idle");
        MovementState = new PlayerMovementState(this, StateMachine, _playerData, "Run");
        JumpState = new PlayerJumpState(this, StateMachine, _playerData, "InAir");
        InAirState = new PlayerInAirState(this, StateMachine, _playerData, "InAir");
        LandState = new PlayerLandState(this, StateMachine, _playerData, "Land");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, _playerData, "WallClimb");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, _playerData, "WallGrab");
        WallSlideSlide = new PlayerWallSlideState(this, StateMachine, _playerData, "WallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, _playerData, "InAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, _playerData, "LedgeClimbState");
        DashState = new PlayerDashState(this, StateMachine, _playerData, "Dash");
        CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, _playerData, "CrouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, _playerData, "CrouchMove");
        GodAbilityState = new PlayerGodAbilityState(this, StateMachine, _playerData, "GodMode");
        ShootState = new PlayerShootState(this, StateMachine, _playerData, "Shoot");
        PullState = new PlayerPullState(this, StateMachine, _playerData, "Pull");
        HyperDashState = new PlayerHyperDashState(this, StateMachine, _playerData, "HyperDash");
        ShieldDashState = new PlayerShieldDashState(this, StateMachine, _playerData, "ShieldDash");

    }

    private void Start()
    {
        InputHandler = GetComponent<PlayerInputHandler>();
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        CapsuleCollider = GetComponent<CapsuleCollider2D>();
        StateMachine.Initialize(IdleState);

    }

    private void Update()
    {
        Core.LogicUpdate();

        StateMachine.CurrentState.StandardUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdate();
    }

    #endregion

    #region Player Methods
    public void SetColliderHeight(float p_height)
    {
        Vector2 center = CapsuleCollider.offset;
        _playerVelocity.Set(CapsuleCollider.size.x, p_height);

        center.y += (p_height - CapsuleCollider.size.y) / 2;
        CapsuleCollider.size = _playerVelocity;
        CapsuleCollider.offset = center;

    }

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishedTrigger() => StateMachine.CurrentState.AnimationFinishedTrigger();


    #endregion

    #region DrawGizmos
    private void OnDrawGizmos()
    {
        if (_debugGizmos)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(Core.CollisionSenses.GroundCheck.position, Core.CollisionSenses.GroundCheckRadius);
            Gizmos.DrawWireSphere(Core.CollisionSenses.CellingCheck.position, Core.CollisionSenses.CellingCheckRadius);



            Gizmos.DrawRay(Core.CollisionSenses.WallCheck.position, Vector2.right * Core.Movement.FacingDirection * Core.CollisionSenses.WallCheckDistance);
            Gizmos.DrawRay(Core.CollisionSenses.LedgeCheck.position, Vector2.right * Core.Movement.FacingDirection * Core.CollisionSenses.WallCheckDistance);

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(Core.CollisionSenses.CellingCheck.position, Core.CollisionSenses.OverlapSize);

        }
    }

    #endregion
}