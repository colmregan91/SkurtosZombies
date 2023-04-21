using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;

public class AnimationBlending : NetworkBehaviour
{
    public Animator _anim;

    [SerializeField] private float _acceleration = 1f;
    [SerializeField] private float _deccceleration = 1f;
    [Networked] public float CurrentXblendValue { get; private set; }
    [Networked] public float CurrentYblendValue { get; private set; }

    [Networked] public Vector2 locaInput { get; private set; }
    private InputData inputdata;

    public override void FixedUpdateNetwork()
    {
        if (Runner.TryGetInputForPlayer<InputData>(Object.InputAuthority, out var input))
        {
            locaInput = input.MoveInput;
        }
        inputdata.MoveInput = locaInput;
    }

    public override void Render()
    {
        UpdateAnimBlending(inputdata.MoveInput);
    }
    public void UpdateAnimBlending(Vector2 input)
    {

        if (input.x > 0.5f) // going right
        {
            CurrentXblendValue += Runner.DeltaTime * _acceleration;
        }
        if (input.x < -0.5f) // going left
        {
            CurrentXblendValue -= Runner.DeltaTime * _acceleration;
        }



        if (input.y > 0.5f) // going forward
        {
            CurrentYblendValue += Runner.DeltaTime * _acceleration;
        }

        if (input.y < -0.5f) //going back
        {
            CurrentYblendValue -= Runner.DeltaTime * _acceleration;
        }

        if (input.y == 0f)
        {
            CurrentYblendValue = Mathf.Lerp(CurrentYblendValue, 0, Runner.DeltaTime * _deccceleration); //todo: Lerp this properly
        }

        if (input.x == 0f)
        {
            CurrentXblendValue = Mathf.Lerp(CurrentXblendValue, 0, Runner.DeltaTime * _deccceleration);//todo: Lerp this properly
        }


        CurrentXblendValue = Mathf.Clamp(CurrentXblendValue, -1f, 1f);
        CurrentYblendValue = Mathf.Clamp(CurrentYblendValue, -1f, 1f);

        _anim.SetFloat(AnimStringUtils.s_BlendXHash, CurrentXblendValue);
        _anim.SetFloat(AnimStringUtils.s_BlendYHash, CurrentYblendValue);
        _anim.SetBool(AnimStringUtils.s_MoveHash, input != Vector2.zero);

    }
}

