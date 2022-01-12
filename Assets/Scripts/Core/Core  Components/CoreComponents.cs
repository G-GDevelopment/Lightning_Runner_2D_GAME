using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComponents : MonoBehaviour
{
    protected Core core;

    protected virtual void Awake()
    {
        core = transform.parent.GetComponent<Core>();

        if(core == null) { Debug.Log("There is no Core on the parent"); }
    }
}
