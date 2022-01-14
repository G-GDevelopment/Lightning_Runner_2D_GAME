using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Core core;

    [SerializeField] private float _speed = 30f;
    [SerializeField] private Rigidbody2D rb;


    private void Start()
    {
        core = GetComponentInParent<Core>();
        rb = GetComponent<Rigidbody2D>();
         rb.velocity = transform.right * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerEnter2D");
        core.Ability.AddToDetected(collision);

        Destroy(gameObject, 0.1f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerExit2D");
        core.Ability.RemoveFromDetected(collision);
    }

}
