using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanguardAbilitySystem : EnemyCoreComponents
{
    [Header("Queue Parameter")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _teleportStartPosition;
    [SerializeField] private bool _recording;
    [SerializeField] private bool _playing;

    private Queue<Vector2> _targetPositions = new Queue<Vector2>();
    private Vector2 _nextPosition;
    private Vector2 _peekPosition;
    private Vector2 _trashPosition;

    [SerializeField] private bool _isPatroling;

    public bool IsPatroling { get => _isPatroling; set => _isPatroling = value; }
    public Transform Target { get => _target; set => _target = value; }
    public Queue<Vector2> TargetPositions { get => _targetPositions; set => _targetPositions = value; }

    public void RecordChasePath()
    {
        _recording = true;
        _targetPositions.Enqueue(_target.position);
    }

    public void StartChase(float p_chaseSpeed)
    {
        _playing = true;
        //_nextPosition = _teleportStartPosition.position;
        _nextPosition = _rb.transform.position;

        while(_nextPosition == (Vector2) _rb.transform.position && _targetPositions.Count > 0)
        {
            //Determine FacingDirection
            _peekPosition = _targetPositions.Peek();
            if(_peekPosition.x < transform.position.x && enemyCore.VanguardMovement.FacingDirection != -1)
            {
                //Left
                enemyCore.VanguardMovement.Flip();
            }
            else if(_peekPosition.x > transform.position.x && enemyCore.VanguardMovement.FacingDirection != 1)
            {
                enemyCore.VanguardMovement.Flip();
            }

            if(Vector2.Distance(_peekPosition, _target.position) > Vector2.Distance(_nextPosition, _target.position))
            {
                _trashPosition = _targetPositions.Dequeue();
            }
            else
            {
                _nextPosition = _targetPositions.Dequeue();

            }
        }

        _rb.transform.position = Vector3.MoveTowards(_rb.transform.position, new Vector3(_nextPosition.x, _nextPosition.y, 0), p_chaseSpeed * Time.deltaTime);
    }
}
