using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName ="Data/Player Date/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Movement Parameter")]
    public float MovementVelocity = 10f;
    public float PlayerDrag = 0f;
    public float ColliderHeightStandard = 0.88f;
    public float ColliderOffsetStandard = -0.01f;

    [Header("Crouch Parameter")]
    public float CrouchMovementVelocity = 5f;
    public float CrouchColliderHeight = 0.45f;
    public float CrouchOffset = -0.22f;

    [Header("Jump Parameter")]
    public float JumpForce = 15f;
    public float FallMultiplier = 2.5f;
    public float LowJumpMultiplier = 2f;
    public int AmountOfJumps = 1;

    [Header("Wall Jump Parameter")]
    public float WallJumpVelocity = 20f;
    public float wallJumpTime = 0.4f;
    public Vector2 WallJumpAngle = new Vector2(1, 2);

    [Header("In Air Parameter")]
    public float CoyoteTime = 0.2f;
    public float VariableJumpHeightMultiplier = 0.5f;

    [Header("WallSlide Parameter")]
    public float WallSlideVelocity = 3.0f;

    [Header("Wall Climb Parameter")]
    public bool ShouldPlayerBeAbleToClimb = false;
    public float WallClimbSpeed = 3.0f;

    [Header("Ledge Climb State")]
    public Vector2 StartOffset;
    public Vector2 StopOffset;

    [Header("Dash Parameter")]
    public float DashCooldown = 0.5f;
    public float DashTime = 0.5f;
    public float MaxHoldtime = 1f;
    public float HoldTimeScale = 0.25f;
    public float DashVelocity = 30f;
    public float Drag = 10f;
    public float DashEndYMultiplier = 0.2f;
    public float DistanceBetweenAfterImages = 0.5f;

    [Header("Ability Parameter")]
    public float HyperDashTime = 0.2f;
    public float ShieldDashTime = 0.5f;
    public float ShieldDashVelocity = 30f;
}
