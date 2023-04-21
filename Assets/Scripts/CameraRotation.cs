using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class CameraRotation : MonoBehaviour
{
    private PlayerController _cont;
    private float _rotationPower = 5f;
    private Transform _followTransform;
    private IHandleinput _input;
    public bool movin;
    Transform _playerTransform => transform.parent;
    public float timeCount = 0.0f;
    public float rotspd = 100f;
    private void Awake()
    {
        _cont = GetComponentInParent<PlayerController>();
        _input = GetComponentInParent<IHandleinput>();
        _followTransform = transform;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        _followTransform.rotation *= Quaternion.AngleAxis(_input.GetLookInput().x * _rotationPower * TimeUTils.instance.getRunnerDelta(), Vector3.up);

        _followTransform.rotation *= Quaternion.AngleAxis(_input.GetLookInput().y * _rotationPower * TimeUTils.instance.getRunnerDelta(), Vector3.right);

        var angles = _followTransform.localEulerAngles;
        angles.z = 0;

        var angle = _followTransform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }


        _followTransform.localEulerAngles = angles;

        if (_input.GetMoveInput() != Vector2.zero)
        {
            RotatePlayer();
        }
        else
        {
            timeCount = 0;
        }
           
    }


    private void RotatePlayer()// follow transform is already moved using delta time in CameraRotation, so it is not needed here
    {
        _playerTransform.rotation = Quaternion.Slerp(_playerTransform.rotation, Quaternion.Euler(0, _followTransform.rotation.eulerAngles.y,0), timeCount);
      
            timeCount = timeCount + Time.deltaTime * rotspd;
        

        //   _playerTransform.rotation = Quaternion.Euler(0, _followTransform.rotation.eulerAngles.y, 0);

        // Reset the y rotation of the look transform
        _followTransform.localEulerAngles = new Vector3(_followTransform.localEulerAngles.x, 0, 0); // todo: try clear this garbage


    }
}
