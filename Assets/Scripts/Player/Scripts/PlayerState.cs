using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected Core core;

    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected bool isAnimationFinished;
    protected bool isExistingState;

    protected float startTime;

    private string _animBoolName;

    public PlayerState(Player p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, string p_animboolName)
    {
        player = p_player;
        stateMachine = p_stateMachine;
        playerData = p_playerData;
        _animBoolName = p_animboolName;
        core = player.Core;

    }

    public virtual void EnterState()
    {
        ChecksForSwitchingState();
        player.Animator.SetBool(_animBoolName, true);
        startTime = Time.time;
        //Debug.Log(_animBoolName);

        isAnimationFinished = false;
        isExistingState = false;
    }

    public virtual void ExitState()
    {
        player.Animator.SetBool(_animBoolName, false);
        isExistingState = true;
    }

    public virtual  void StandardUpdate() { }

    public virtual void FixedUpdate()
    {
        ChecksForSwitchingState();
    }

    public virtual void ChecksForSwitchingState() { }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishedTrigger() => isAnimationFinished = true;
}
