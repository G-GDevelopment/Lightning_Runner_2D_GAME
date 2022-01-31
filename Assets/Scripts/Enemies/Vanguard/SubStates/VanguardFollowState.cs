using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanguardFollowState : VanguardActiveState
{
    public VanguardFollowState(Vanguard p_vanguard, VanguardStateMachine p_stateMachine, VanguardData p_data, string p_animBoolName) : base(p_vanguard, p_stateMachine, p_data, p_animBoolName)
    {
    }
    public override void CheckForSwitchingStates()
    {
        base.CheckForSwitchingStates();
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        core.VanguardAbility.RecordChasePath();

        if (Time.time > startTime + vanguardData.WaitTimeForFollowing)
        {
            //Teleport to Following Position

            //Start Following player by copying movement
            core.VanguardAbility.StartChase(vanguardData.ChaseSpeed);
        }
    }

    public override void StandardUpdate()
    {
        base.StandardUpdate();



        
    }
}
