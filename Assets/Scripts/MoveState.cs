using System;
using UnityEngine;

public class MoveState : IState
{
    private Transform m_followTransform;
    private Transform m_playerTransform;
    private InputController m_input;
    private float m_speed = 1f;

    // Comment out unused method
    // public Tuple<float,float> GetBlendValues()
    // {
    //     return new Tuple<float, float>(_currentXblendValue, _currentYblendValue);
    // }

    public MoveState(Transform followTransform, Transform playerTransform, InputController input)
    {
        m_followTransform = followTransform;
        m_playerTransform = playerTransform;
        m_input = input;
    }

    public void OnEnter()
    {
        Debug.Log("Move state entered");
    }

    public void OnExit()
    {
        Debug.Log("Move state exited");
    }

    private void MovePlayer()
    {
        float moveSpeed = m_speed / 100f;
        Vector3 position = (m_playerTransform.forward * m_input.GetMoveInput().y * moveSpeed) + (m_playerTransform.right * m_input.GetMoveInput().x * moveSpeed);
        m_playerTransform.position += position;
    }

    private void RotatePlayer()
    {
        // Set the player rotation based on the look transform
        m_playerTransform.rotation = Quaternion.Euler(0, m_followTransform.rotation.eulerAngles.y, 0);

        // Reset the y rotation of the look transform
        m_followTransform.localEulerAngles = new Vector3(m_followTransform.localEulerAngles.x, 0, 0);
    }

    public void OnUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }
}
