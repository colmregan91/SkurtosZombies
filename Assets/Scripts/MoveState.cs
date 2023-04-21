using System;
using UnityEngine;

public class MoveState : IState
{
    private Transform _followTransform;
    private Transform _playerTransform;
    private IHandleinput _input;
    private float _speed = 2f;
    private Vector2 _moveInputVector;

    private Vector3 _nextPosition;



    public MoveState(Transform followTransform, Transform playerTransform, IHandleinput input)
    {
        _followTransform = followTransform;
        _playerTransform = playerTransform;
        _input = input;

    }

    public void OnEnter()
    {
        Debug.Log("Move state entered");
    }

    public void OnExit()
    {
        Debug.Log("Move state exited");
    }

    private void MovePlayer()
    {
        _moveInputVector = _input.GetMoveInput();
        _nextPosition = ((_playerTransform.forward * _moveInputVector.y * _speed) + (_playerTransform.right * _moveInputVector.x * _speed)) * TimeUTils.instance.getRunnerDelta();
        _playerTransform.position += _nextPosition;
        
    }


    public void OnUpdate()
    {
        MovePlayer();
    }
}
