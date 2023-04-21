
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private StateMachine _playerStateMachine;
    private MoveState _moveState;
    private IdleState _idleState;
    private AnimationBlending _animBlending;
    private IHandleinput _input;

    [SerializeField] private Transform FollowTransform;
    private Transform _playerTransform;


    public Action<IState> OnPlayerStateChanged;

    [SerializeField] private string state;

    public void UpdateString(IState NEwstate)
    {
        state = NEwstate.ToString();
    }


    public void Init()
    {
        _input = GetComponent<IHandleinput>();
        _animBlending = GetComponentInChildren<AnimationBlending>();
              _playerTransform = transform;
        _playerStateMachine = new StateMachine();
        _moveState = new MoveState(FollowTransform, _playerTransform, _input);
        _idleState = new IdleState();


        _playerStateMachine.OnStateChanged += (state) => OnPlayerStateChanged?.Invoke(state);

        _playerStateMachine.AddTransition(_moveState, _idleState, IsMoving);
        _playerStateMachine.AddTransition(_idleState, _moveState, IsNotMoving);

        _playerStateMachine.Init(_idleState);

    }

    private void Start()
    {

    }


    // Update is called once per frame
    public void UpdateStateMachine()
    {
        _playerStateMachine.Tick();
    }

    public bool IsMoving()
    {
        return _input.GetMoveInput() != Vector2.zero;

    }
    public bool IsNotMoving()
    {
        return _input.GetMoveInput() == Vector2.zero;
    }

    private void OnDisable()
    {
        _playerStateMachine.OnStateChanged -= (state) => OnPlayerStateChanged?.Invoke(state);
    }
}
