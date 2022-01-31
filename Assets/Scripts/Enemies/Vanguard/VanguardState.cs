using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanguardState
{
    protected EnemyCore core;

    protected Vanguard vanguard;
    protected VanguardStateMachine stateMachine;
    protected VanguardData vanguardData;

    protected bool isExistingState;

    protected float startTime;

    private string _animBoolName;

    public VanguardState(Vanguard p_vanguard, VanguardStateMachine p_stateMachine, VanguardData p_data, string p_animBoolName)
    {
        vanguard = p_vanguard;
        stateMachine = p_stateMachine;
        vanguardData = p_data;
        _animBoolName = p_animBoolName;

        core = vanguard.Core;
    }

    public virtual void EnterState()
    {
        CheckForSwitchingStates();
        vanguard.Animator.SetBool(_animBoolName, true);
        startTime = Time.time;
        Debug.Log(_animBoolName);

        isExistingState = false;
    }

    public virtual void ExitState()
    {
        vanguard.Animator.SetBool(_animBoolName, false);
        isExistingState = true;
    }

    public virtual void StandardUpdate() { }

    public virtual void FixedUpdate()
    {
        CheckForSwitchingStates();
    }

    public virtual void CheckForSwitchingStates() { }

}
