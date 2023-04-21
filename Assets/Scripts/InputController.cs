using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour, IHandleinput
{
    [SerializeField] private Transform _camTransform;
    [SerializeField] private PlayerStateMachine _playerStateMachine;
    [SerializeField] private CameraRotation _camRotation;
    [SerializeField] private AnimationBlending _animBlending;

    private InputAsset _defaultPlayersActions;
    private InputAction _moveAction;
    private InputAction _lookAction;

    private Vector2 _move => _moveAction.ReadValue<Vector2>();
    private Vector2 _look => _lookAction.ReadValue<Vector2>();

    public Vector2 GetMoveInput()
    {
        return _move;
    }

    public Vector2 GetLookInput()
    {
        return _look;
    }
    private void Awake()
    {
        _defaultPlayersActions = new InputAsset();

        _camTransform.SetParent(null);
    }

    private void OnEnable()
    {
        _moveAction = _defaultPlayersActions.Player.Move;
        _moveAction.Enable();

        _lookAction = _defaultPlayersActions.Player.Look;
        _lookAction.Enable();
    }



    private void OnDisable()
    {
        _moveAction.Disable();
        _lookAction.Disable();
    }
}
