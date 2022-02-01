using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GodRemnant : MonoBehaviour, IDamageable
{

    private Seeker _seeker;
    private Rigidbody2D rb;

    [Header("AI Stats")]
    [SerializeField] private Transform _target;
    [SerializeField] private float _aISpeed = 200f;
    [SerializeField] private float _redirectSpeed = 100f;
    [SerializeField] private bool _attackPlayerNow;
    [Space]
    [Header("Custom Behavior")]
    [SerializeField] public bool _followEnabled = true;
    [SerializeField] private float _pathUpdateSeconds = 0.5f;
    [SerializeField] private float _nextWayPointDistance = 3f;
    [SerializeField] private float _activeDistance = 50f;
    [Space]
    [SerializeField] private GameObject _enemySprite;
    private Path _currentPath;
    private int _currentWayPoint = 0;
    private bool _reachedEndOfPath = false;

    public bool destroyThisObject = false;
    public void Damage(float amount)
    {
        Debug.Log(amount + " Damage taken");

        Destroy(gameObject, 0.0f);
    }

    public void Dashed(bool isDashed)
    {
        throw new System.NotImplementedException();
    }

    public void Pulled(bool isPulled)
    {
        throw new System.NotImplementedException();
    }
    public void Start()
    {
        _seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        //This update the Pathfinding Function every 0.5 seconds
        InvokeRepeating("UpdatePath", 0f, _pathUpdateSeconds);


        //This will make the track the player
        //OnPathComplete is another function that tells when Ai has Reached Player
        _seeker.StartPath(rb.position, _target.position, OnPathComplete);
    }
    void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            _currentPath = path;
            _currentWayPoint = 0;
        }
    }

    private void FixedUpdate()
    {


        //PathFinding
        if (TargetInDistance() && _followEnabled && !_attackPlayerNow)
        {
            PathFollow();
        }
    }

    private void PathFollow()
    {
        //Checking if there is path, for the AI to follow
        if (_currentPath == null)
        {
            return;
        }
        if (_currentWayPoint >= _currentPath.vectorPath.Count)
        {
            _reachedEndOfPath = true;
            return;
        }
        else
        {
            _reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)_currentPath.vectorPath[_currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * _aISpeed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, _currentPath.vectorPath[_currentWayPoint]);

        if (distance < _nextWayPointDistance)
        {
            _currentWayPoint++;
        }
        #region Determine FacingDirection
  /*      //Flipping the Sprite
        if (force.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            _side = 1;
        }
        else if (force.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            _side = -1;
        }

        if (force.y >= 0.01f)
        {
            _sideY = 1;
        }
        else if (force.y <= -0.01f)
        {
            _sideY = -1;
        } */
        #endregion
    }
    private void UpdatePath()
    {
        if (_seeker.IsDone())
        {
            _seeker.StartPath(rb.position, _target.position, OnPathComplete);
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, _target.transform.position) < _activeDistance;
    }


    //Void Atack Pattern
}
