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
    private InputController _input;

    [SerializeField]private Transform FollowTransform;
    private Transform PlayerTransform;


    public static Action<IState> OnPlayerStateChanged;

    
    private void Awake()
    {
        _input = GetComponent<InputController>();
        _animBlending = GetComponentInChildren<AnimationBlending>();
        PlayerTransform = transform;
        _playerStateMachine = new StateMachine();
        _moveState = new MoveState(FollowTransform, PlayerTransform, _input);
        _idleState = new IdleState(FollowTransform, _input);
    }
    // Start is called before the first frame update
    void Start()
    {
        _playerStateMachine.OnStateChanged += (state) => OnPlayerStateChanged?.Invoke(state);

        _playerStateMachine.AddTransition(_moveState, _idleState, IsMoving);
        _playerStateMachine.AddTransition(_idleState, _moveState, IsNotMoving);

        _playerStateMachine.Init(_idleState);
    }

    // Update is called once per frame
    void Update()
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
