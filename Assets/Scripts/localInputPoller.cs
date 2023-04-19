using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using UnityEngine.InputSystem;

public class localInputPoller : NetworkBehaviour, IBeforeUpdate
{
    private Vector2 MoveinputVector;
    private Vector2 LookinputVector;

    private bool IsLocalPlayer;

    private InputAsset _defaultPlayersActions;
    private InputAction _moveAction;
    private InputAction _lookAction;
    private NetworkEvents evs;
    private Vector2 _move => _moveAction.ReadValue<Vector2>();
    private Vector2 _look => _lookAction.ReadValue<Vector2>();




    public Vector2 GetLocalPlayerMoveInput()
    {
        return _move;
    }

    public Vector2 GetLocalPlayerLookInput()
    {
        return _look;
    }

    public override void Spawned()
    {
        IsLocalPlayer = Runner.LocalPlayer == Object.HasInputAuthority;

        if (!IsLocalPlayer)
        {
            this.enabled = false;
            return;
        }
        _defaultPlayersActions = new InputAsset();
        _moveAction = _defaultPlayersActions.Player.Move;
        _moveAction.Enable();

        _lookAction = _defaultPlayersActions.Player.Look;
        _lookAction.Enable();
        //         Runner.AddCallbacks(this);
        evs = Runner.GetComponent<NetworkEvents>();
        evs.OnInput.AddListener(OnInput);
        evs.OnShutdown.AddListener(OnShutdown);
    }



    public void BeforeUpdate()
    {
        if (IsLocalPlayer)
        {
            MoveinputVector = GetLocalPlayerMoveInput();
            LookinputVector = GetLocalPlayerLookInput();
        }

    }


    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        if (Runner != null && Runner.IsRunning)
        {
            var inputdata = GetInputData();
            input.Set(inputdata);
        }
    }
    public InputData GetInputData()
    {
        InputData data = new InputData();
        data.MoveInput = MoveinputVector;
        data.LookInput = LookinputVector;
        return data;
    }


    void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        if (IsLocalPlayer)
        {
            evs.OnInput.RemoveListener(OnInput);
            evs.OnShutdown.RemoveListener(OnShutdown);
        }

    }


    private void OnDisable()
    {
        if (_moveAction != null)
        {
            _moveAction.Disable();

        }
        if (_lookAction != null)
        {
            _lookAction.Disable();
        }


    }
}
