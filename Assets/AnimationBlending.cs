using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;

public class AnimationBlending : NetworkBehaviour
{

    private IHandleinput _input;
    public Animator _anim;

    [SerializeField] private float _acceleration = 1f;
    [SerializeField] private float _deccceleration = 1f;
    private Vector3 _inputVector;

    public float CurrentXblendValue { get; private set; }
    public float CurrentYblendValue { get; private set; }


    private PlayerStateMachine _PlayerStateMachine;

    void ONPlayerStateChanged(IState state)
    {
        if (state is IdleState)
        {
            _anim.SetBool(AnimStringUtils.s_MoveHash, false);
        }
        if (state is MoveState)
        {
            _anim.SetBool(AnimStringUtils.s_MoveHash, true);
        }

    }
    public void Init()
    {
        _input = GetComponentInParent<IHandleinput>();
        _anim = GetComponent<Animator>();
        _PlayerStateMachine = GetComponentInParent<PlayerStateMachine>();

        _PlayerStateMachine.OnPlayerStateChanged += ONPlayerStateChanged;
    }

    private void OnDisable()
    {
        _PlayerStateMachine.OnPlayerStateChanged -= ONPlayerStateChanged;
    }

    // Update is called once per frame
    public void UpdateAnimBlending(float DeltaTime)
    {

        _inputVector = _input.GetMoveInput();

        if (_inputVector.x > 0.5f) // going right
        {
            CurrentXblendValue += DeltaTime * _acceleration;
        }
        if (_inputVector.x < -0.5f) // going left
        {
            CurrentXblendValue -= DeltaTime * _acceleration;
        }



        if (_inputVector.y > 0.5f) // going forward
        {
            CurrentYblendValue += DeltaTime * _acceleration;
        }

        if (_inputVector.y < -0.5f) //going back
        {
            CurrentYblendValue -= DeltaTime * _acceleration;
        }

        if (_inputVector.y == 0f)
        {
            CurrentYblendValue = Mathf.Lerp(CurrentYblendValue, 0, DeltaTime * _deccceleration); //todo: Lerp this properly
        }

        if (_inputVector.x == 0f)
        {
            CurrentXblendValue = Mathf.Lerp(CurrentXblendValue, 0, DeltaTime * _deccceleration);//todo: Lerp this properly
        }


        CurrentXblendValue = Mathf.Clamp(CurrentXblendValue, -1f, 1f);
        CurrentYblendValue = Mathf.Clamp(CurrentYblendValue, -1f, 1f);

        _anim.SetFloat(AnimStringUtils.s_BlendXHash, CurrentXblendValue);
        _anim.SetFloat(AnimStringUtils.s_BlendYHash, CurrentYblendValue);


    }
}
