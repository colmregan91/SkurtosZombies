using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerController : NetworkBehaviour, IHandleinput
{
    [SerializeField] private Transform _camTransform;
    [SerializeField] private PlayerStateMachine _playerStateMachine;
    [SerializeField] private CameraRotation _camRot;
    [SerializeField] private AnimationBlending _animBlending;

    private const string LOCALNAME = "localPlayer";
    private const string REMOTENAME = "remotePlayer";

    private Vector2 _moveInput;
    private Vector2 _lookInput;

    public Vector2 GetMoveInput()
    {
        return _moveInput;
    }

    public Vector2 GetLookInput()
    {
        return _lookInput;
    }

    public override void Spawned()
    {
        _playerStateMachine.Init();
        _camRot.Init();
        _animBlending.Init();

        if (Runner.LocalPlayer == Object.HasInputAuthority)
        {
            _camTransform.SetParent(null);

            gameObject.name = LOCALNAME;

        }
        else
        {
            disableComponents();
            gameObject.name = REMOTENAME;
        }
    }

    private void disableComponents()
    {
        _camRot.enabled = false;
        _camTransform.gameObject.SetActive(false);
    }


    public override void FixedUpdateNetwork()
    {
        if (Runner.TryGetInputForPlayer<InputData>(Object.InputAuthority, out var input))
        {
            Debug.Log("here" + gameObject.name);
            _moveInput = input.MoveInput;
            _lookInput = input.LookInput;

            _playerStateMachine.UpdateStateMachine(Runner.DeltaTime);
        
            //    CamRot.UpdateFollowCam(Runner.DeltaTime);
        }

        _animBlending.UpdateAnimBlending(Runner.DeltaTime);
        Debug.Log(_animBlending.CurrentXblendValue);
        Debug.Log(_animBlending._anim.GetFloat(AnimStringUtils.s_BlendXHash));

        //if (Runner.TryGetInputForPlayer<InputData>(Object.InputAuthority, out var input))
        //{
        //    Debug.Log("here" + gameObject.name);
        //    MoveInput = input.MoveInput;
        //    LookInput = input.LookInput;

        //    PlayerStateMachine.UpdateStateMachine(Runner.DeltaTime);
        //    CamRot.UpdateFollowCam(Runner.DeltaTime);
        //  //  AnimBlending.UpdateAnimBlending(Runner.DeltaTime);
        //}
    }
}
