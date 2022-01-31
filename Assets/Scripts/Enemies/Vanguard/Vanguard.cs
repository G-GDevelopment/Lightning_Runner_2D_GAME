using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vanguard : MonoBehaviour
{
    #region Variables Concerning States
    public VanguardStateMachine StateMachine { get; private set; }
    public VanguardIdleState IdleState { get; private set; }
    public VanguardFollowState FollowState { get; private set; }
    public VanguardPatrolState PatrolState { get; private set; }


    [SerializeField]
    private VanguardData _vanguardData;
    #endregion

    #region Components
    public EnemyCore Core { get; private set; }
    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public CapsuleCollider2D CapsuleCollider { get; private set; }

    #endregion

    #region Variables

    [SerializeField] private bool _debugGizmos = false;
    #endregion

    #region Built In Method
    private void Awake()
    {
        Core = GetComponentInChildren<EnemyCore>();

        StateMachine = new VanguardStateMachine();
        IdleState = new VanguardIdleState(this, StateMachine, _vanguardData, "Idle");
        PatrolState = new VanguardPatrolState(this, StateMachine, _vanguardData, "Patrol");
        FollowState = new VanguardFollowState(this, StateMachine, _vanguardData, "Move");



    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
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

    #region Enemy Methods

    #endregion

    #region DrawGizmos
    private void OnDrawGizmos()
    {
        if (_debugGizmos)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(Core.CollisionSystem.GroundChecker.position, Core.CollisionSystem.GroundCheckRadius);

            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(Core.CollisionSystem.WallChecker.position, Vector2.right * Core.VanguardMovement.FacingDirection * Core.CollisionSystem.WallCheckDistance);
            Gizmos.DrawRay(Core.CollisionSystem.EdgeChecker.position, Vector2.down * Core.VanguardMovement.FacingDirection * Core.CollisionSystem.EdgeCheckDistance);

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + (Vector3)Core.CollisionSystem.OffsetBox, Core.CollisionSystem.CollisionSize);


        }
    }

    #endregion
}
