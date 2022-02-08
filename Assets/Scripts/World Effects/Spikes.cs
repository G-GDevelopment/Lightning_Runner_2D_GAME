using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private int _enemyVanguardLayer;
    [SerializeField] private int _godRemnant;
    [SerializeField] private int _player;

    private List<IDamageable> _detectedDamageable = new List<IDamageable>();

    private void Update()
    {
        CheckForDamage();
    }

    public void AddToDetected(Collider2D p_collision)
    {
        //Debug.Log("AddedToDetected");
        IDamageable damageable = p_collision.GetComponent<IDamageable>();
        IWorld worldEffects = p_collision.GetComponent<IWorld>();

        if (damageable != null)
        {
            //Debug.Log("Added");
            _detectedDamageable.Add(damageable);
        }
    }

    public void RemoveFromDetected(Collider2D p_collision)
    {
        //Debug.Log("RemoveFromDetected");
        IDamageable damageable = p_collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            //Debug.Log("Removed");
            _detectedDamageable.Remove(damageable);
        }
    }

    private void CheckForDamage()
    {

        foreach (IDamageable item in _detectedDamageable.ToList())
        {
            item.Damage(1f);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _enemyVanguardLayer || collision.gameObject.layer == _godRemnant || collision.gameObject.layer == _player)
        {
            AddToDetected(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _enemyVanguardLayer || collision.gameObject.layer == _godRemnant || collision.gameObject.layer == _player)
        {
            RemoveFromDetected(collision);
        }
    }
}
