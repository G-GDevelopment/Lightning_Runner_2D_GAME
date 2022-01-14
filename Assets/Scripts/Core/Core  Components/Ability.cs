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
    public List<IDamageable> DetectedDamageable { get => _detectedDamageable; set => _detectedDamageable = value; }

    [SerializeField] private bool _hasGodRemnant;
    [SerializeField] private bool _hasObtainedPullShoot;
    [SerializeField] private bool _hasObtainedHyperDash;
    [SerializeField] private bool _hasObtainedShieldDash;

    [SerializeField] private Transform _shootPullPoint;
    [SerializeField] private GameObject _shootPrefab;

    private List<IDamageable> _detectedDamageable = new List<IDamageable>();

    public void RefillGodRemant() => _hasGodRemnant = true;
    public void UseGodRemnant() => _hasGodRemnant = false;
    public void LogicUpdate()
    {
        CheckForDamage();
    }
    public void Shoot()
    {
        GameObject projectile = Instantiate(_shootPrefab, _shootPullPoint.position, _shootPullPoint.rotation) as GameObject;
        projectile.transform.parent = gameObject.transform;

        UseGodRemnant();
    }

    public void AddToDetected(Collider2D collision)
    {
        //Debug.Log("AddedToDetected");
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            //Debug.Log("Added");
            _detectedDamageable.Add(damageable);
        }
    }

    public void RemoveFromDetected(Collider2D collision)
    {
        //Debug.Log("RemoveFromDetected");
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            //Debug.Log("Removed");
            _detectedDamageable.Remove(damageable);
        }
    }

    private void CheckForDamage()
    {
        //Variable that change the Damageamount

        foreach (IDamageable item in _detectedDamageable)
        {
            item.Damage(1f);
        }
    }
}
