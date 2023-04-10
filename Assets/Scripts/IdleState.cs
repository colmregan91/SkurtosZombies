using UnityEngine;

public class IdleState : IState
{
    private Transform _followTransform;
    private InputController _input;
    private float _rotationPower = 0.1f;

    private float _rotationLerp = 0.5f;

    public IdleState(Transform followTrans, InputController input)
    {
        _followTransform = followTrans;
        _input = input;
    }
    public void OnEnter()
    {

        //_anim.SetFloat(AnimStringUtils.blendXHash, 0);
        //_anim.SetFloat(AnimStringUtils.blendYHash, 0);
        Debug.Log("Idle state entered");
    }

    public void OnExit()
    {
        Debug.Log("Idle state exited");
    }

    public void OnUpdate()
    {
        //_followTransform.transform.rotation *= Quaternion.AngleAxis(_input.GetLookInput().x * rotationPower, Vector3.up);


        //_followTransform.transform.rotation *= Quaternion.AngleAxis(_input.GetLookInput().y * rotationPower, Vector3.right);

        //var angles = _followTransform.transform.localEulerAngles;
        //angles.z = 0;

        //var angle = _followTransform.transform.localEulerAngles.x;

        ////Clamp the Up/Down rotation
        //if (angle > 180 && angle < 340)
        //{
        //    angles.x = 340;
        //}
        //else if (angle < 180 && angle > 40)
        //{
        //    angles.x = 40;
        //}

        //_followTransform.transform.localEulerAngles = angles;

        //if (aimValue == 1)
        //{
        //    //Set the player rotation based on the look transform
        //    transform.rotation = Quaternion.Euler(0, _followTransform.transform.rotation.eulerAngles.y, 0);
        //    //reset the y rotation of the look transform
        //    followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
        //}
    }
}
