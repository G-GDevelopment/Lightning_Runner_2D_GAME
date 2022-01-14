using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodRemnant : MonoBehaviour, IDamageable
{

    public void Damage(float amount)
    {
        Debug.Log(amount + " Damage taken");
    }

    private void Update()
    {
        
    }
}
