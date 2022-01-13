using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : CoreComponents
{
    
    public bool HasGodRemnant { get => _hasGodRemnant; set => _hasGodRemnant = value; }
    public bool HasObtainedPullShoot { get => _hasObtainedPullShoot; set => _hasObtainedPullShoot = value; }
    public bool HasObtainedHyperDash { get => _hasObtainedHyperDash; set => _hasObtainedHyperDash = value; }
    public bool HasObtainedShieldDash { get => _hasObtainedShieldDash; set => _hasObtainedShieldDash = value; }
    public Transform ShootPullPoint { get => _shootPullPoint; set => _shootPullPoint = value; }

    [SerializeField] private bool _hasGodRemnant;
    [SerializeField] private bool _hasObtainedPullShoot;
    [SerializeField] private bool _hasObtainedHyperDash;
    [SerializeField] private bool _hasObtainedShieldDash;

    [SerializeField] private Transform _shootPullPoint;
    [SerializeField] private GameObject _shootPrefab;

    public void LogicUpdate()
    {

    }

    public void RefillGodRemant() => _hasGodRemnant = true;
    public void UseGodRemnant() => _hasGodRemnant = false;
}
