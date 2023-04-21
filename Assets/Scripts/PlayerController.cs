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

    private const string LOCALNAME = "localPlayer";
    private const string REMOTENAME = "remotePlayer";

    private Vector2 MoveInput;
    private Vector2 LookInput;

    private float deltaTime => Runner.DeltaTime;

    public float DeltaTime()
    {
        return deltaTime;
    }
    public Vector2 GetMoveInput()
    {
        return MoveInput;
    }

    public Vector2 GetLookInput()
    {
        return LookInput;
    }

    public override void Spawned()
    {
        _playerStateMachine.Init();
        if (Runner.LocalPlayer == Object.HasInputAuthority)
        {
           
            _camTransform.SetParent(null);

            gameObject.name = LOCALNAME;

        }
        else
        {
            DisableComponents();
            gameObject.name = REMOTENAME;
        }
    }

    private void DisableComponents()
    {
        _camTransform.gameObject.SetActive(false);
    }

    public override void FixedUpdateNetwork()
    {
        if (Runner.TryGetInputForPlayer<InputData>(Object.InputAuthority, out var input))
        {
            MoveInput = input.MoveInput;
            LookInput = input.LookInput;

            _playerStateMachine.UpdateStateMachine();

        }

    }
}
