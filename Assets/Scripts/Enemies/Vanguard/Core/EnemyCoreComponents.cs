using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCoreComponents : MonoBehaviour
{
    protected EnemyCore enemyCore;

    protected virtual void Awake()
    {
        enemyCore = transform.parent.GetComponent<EnemyCore>();

        if (enemyCore == null) { Debug.Log("There is no Core on the parent"); }
    }
}
