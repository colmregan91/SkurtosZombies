using System;
using UnityEngine;

public class MoveState : IState
{
    private Transform _followTransform;
    private Transform _playerTransform;
    private InputController _input;
    private float _speed = 1f;


    //public Tuple<float,float> getBlendValues()
    //{
    //    return new Tuple<float, float>(_currentXblendValue, _currentYblendValue);
    //}

    public MoveState(Transform followTrans, Transform playerTransform, InputController input)
    {
        _followTransform = followTrans;
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

    public void OnUpdate()
    {

        float moveSpeed = _speed / 100f;
        Vector3 position = (_playerTransform.forward * _input.GetMoveInput().y * moveSpeed) + (_playerTransform.right * _input.GetMoveInput().x * moveSpeed);
        _playerTransform.position += position;


        //var quat = Quaternion.LookRotation(_input.GetMoveInput().normalized);
        //_playerTransform.rotation = Quaternion.RotateTowards(_playerTransform.rotation, quat, 0);

        ////Set the player rotation based on the look transform
        
        _playerTransform.rotation = Quaternion.Euler(0, _followTransform.rotation.eulerAngles.y, 0);

        ////reset the y rotation of the look transform
        _followTransform.localEulerAngles = new Vector3(_followTransform.localEulerAngles.x, 0, 0);



    }
}

