using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperDashProjectile : MonoBehaviour
{
    [SerializeField] private Core core;

    [SerializeField] private float _speed = 30f;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private LayerMask _godRemnantLayer; //9
    [SerializeField] private LayerMask _ironLayer; //10


    private void Start()
    {
        core = GetComponentInParent<Core>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerEnter2D");
        if(collision.gameObject.layer == 9 || collision.gameObject.layer == 10)
        {
            core.Ability.AddToDetected(collision);
            core.Ability.SaveHyperDashPosition(collision.gameObject.transform.position);
            core.Ability.SetFoundPositionToTrue();
        }

        Destroy(gameObject, 0.1f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerExit2D");
        core.Ability.RemoveFromDetected(collision);
    }
}
