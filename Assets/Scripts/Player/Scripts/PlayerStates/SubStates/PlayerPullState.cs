using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPullState : PlayerAblilityState
{
    public PlayerPullState(Player p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, string p_animboolName) : base(p_player, p_stateMachine, p_playerData, p_animboolName)
    {
    }
}