using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    private float _rotationPower = 5f;

    private Transform _followTransform;
    private IHandleinput _input;

    public void Init()
    {
        _input = GetComponentInParent<IHandleinput>();
        _followTransform = transform;
    }


    // Update is called once per frame
    public void UpdateFollowCam(float DeltaTime)
    {
        _followTransform.rotation *= Quaternion.AngleAxis(_input.GetLookInput().x * _rotationPower * DeltaTime, Vector3.up);

        _followTransform.rotation *= Quaternion.AngleAxis(_input.GetLookInput().y * _rotationPower * DeltaTime, Vector3.right);

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
    }
}
