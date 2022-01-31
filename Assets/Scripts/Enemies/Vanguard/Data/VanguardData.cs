using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Date/Base Data")]
public class VanguardData : ScriptableObject
{
    [Header("Movement Parameter")]
    public float MovementSpeed = 4.0f;


    [Header("Follow Parameter")]
    public float WaitTimeForFollowing = 0.2f;
    public float ChaseSpeed = 50.0f;
}
