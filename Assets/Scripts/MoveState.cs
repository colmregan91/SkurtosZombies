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

    private void MovePlayer(float DeltaTime)
    {
        _moveInputVector = _input.GetMoveInput();
        _nextPosition = ((_playerTransform.forward * _moveInputVector.y * _speed) + (_playerTransform.right * _moveInputVector.x * _speed)) *  DeltaTime;
        _playerTransform.position += _nextPosition;
    }

    private void RotatePlayer()// follow transform is already moved using delta time in CameraRotation, so it is not needed here
    {
   
        _playerTransform.rotation = Quaternion.Euler(0, _followTransform.rotation.eulerAngles.y, 0); 

        // Reset the y rotation of the look transform
        _followTransform.localEulerAngles = new Vector3(_followTransform.localEulerAngles.x, 0, 0); // todo: try clear this garbage
    }

    public void OnUpdate(float DeltaTime)
    {
        MovePlayer(DeltaTime);
        RotatePlayer();
    }
}
