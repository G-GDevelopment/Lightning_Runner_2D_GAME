using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform _startPosition, _positionA, _positionB;
    [SerializeField] private Vector3 _nextPosition;
    [Header("Moving Platforms Parameter")]
    [SerializeField] private float _speed;
    [SerializeField] private bool returnBack = true;

    [SerializeField] private int _playerLayer;
    public bool _movePlatform = true;


    // Start is called before the first frame update
    void Start()
    {
        //Auto Find Positions
        _startPosition = gameObject.transform;

        _startPosition.position = _positionA.position;
        _nextPosition = _startPosition.position;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovingThePlatformBackAndForth(_movePlatform);
    }


    private void OnDrawGizmoOnSelected()
    {
        Gizmos.DrawLine(_positionA.position, _positionB.position);
    }

    private void MovingThePlatformBackAndForth(bool canMove)
    {
        if (canMove)
        {
            if (transform.position == _positionA.position)
            {
                _nextPosition = _positionB.position;
            }
            if (returnBack)
            {
                if (transform.position == _positionB.position)
                {
                    _nextPosition = _positionA.position;
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, _nextPosition, _speed * Time.deltaTime);
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _playerLayer)
        {
            if (!_movePlatform)
                _movePlatform = true;
            //Glue the Player to the moving platform
            collision.collider.transform.SetParent(transform);

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _playerLayer)
        {
            //Glue the Player to the moving platform
            collision.collider.transform.SetParent(null);

        }
    }
}
