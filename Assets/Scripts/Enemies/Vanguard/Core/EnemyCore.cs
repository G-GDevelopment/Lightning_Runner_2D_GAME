using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCore : MonoBehaviour
{
    public VanguardMovement VanguardMovement { get; private set; }
    public VanguardCollisionSystem CollisionSystem { get; private set; }
    public VanguardAbilitySystem VanguardAbility { get; private set; }
    public Animator Anim { get; private set; }


    private void Awake()
    {
        VanguardMovement = GetComponentInChildren<VanguardMovement>();
        CollisionSystem = GetComponentInChildren<VanguardCollisionSystem>();
        VanguardAbility = GetComponentInChildren<VanguardAbilitySystem>();
        Anim = GetComponentInParent<Animator>();

        if (!VanguardMovement || !CollisionSystem|| !VanguardAbility)
        {
            Debug.Log("Missing Core Components");
        }

        if (!Anim)
        {
            Debug.Log("Missing Animator Component");
        }

    }

    public void LogicUpdate()
    {
        VanguardMovement.LogicUpdate();
    }

}
