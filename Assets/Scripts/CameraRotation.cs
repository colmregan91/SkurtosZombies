using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [HeaderAttribute("PUBLIC")]
    public float rotationPower = 0.1f;

    [HeaderAttribute("PRIVATE")]
    private Transform _followTransform;
    private InputController _input;

    private void Awake()
    {
        _input = GetComponentInParent<InputController>(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        _followTransform = transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _followTransform.rotation *= Quaternion.AngleAxis(_input.GetLookInput().x * rotationPower, Vector3.up);

        _followTransform.rotation *= Quaternion.AngleAxis(_input.GetLookInput().y * rotationPower, Vector3.right);

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
