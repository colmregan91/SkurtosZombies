using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBlending : MonoBehaviour
{

    private InputController _input;
    private Animator _anim;

    [SerializeField]private float _acceleration = 1f;
    [SerializeField] private float _deccceleration = 1f;
    private Vector3 _inputVector;
    public float currentXblendValue { get; private set; }
    public float currentYblendValue { get; private set; }

    void ONPlayerStateChanged(IState state)
    {
        if (state is IdleState)
        {
            _anim.SetBool(AnimStringUtils.moveHash, false);
        }
        if (state is MoveState)
        {
            _anim.SetBool(AnimStringUtils.moveHash, true);
        }

    }
    private void Awake()
    {
        _input = GetComponentInParent<InputController>();
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        PlayerStateMachine.OnPlayerStateChanged += ONPlayerStateChanged;
    }

    private void OnDisable()
    {
        PlayerStateMachine.OnPlayerStateChanged -= ONPlayerStateChanged;
    }

    // Update is called once per frame
    void Update()
    {

        _inputVector = _input.GetMoveInput();
        
        if (_inputVector.x > 0.5f) // going right
        {
            currentXblendValue += Time.deltaTime * _acceleration;
        }
        if (_inputVector.x < -0.5f) // going left
        {
            currentXblendValue -= Time.deltaTime * _acceleration;
        }



        if (_inputVector.y > 0.5f) // going forward
        {
            currentYblendValue += Time.deltaTime * _acceleration;
        }

        if (_inputVector.y < -0.5f) //going back
        {
            currentYblendValue -= Time.deltaTime * _acceleration;
        }

        if (_inputVector.y == 0f) 
        {
            currentYblendValue = Mathf.Lerp(currentYblendValue, 0, Time.deltaTime * _deccceleration); //todo: Lerp this properly
        }

        if (_inputVector.x == 0f)
        {
            currentXblendValue = Mathf.Lerp(currentXblendValue, 0, Time.deltaTime * _deccceleration);//todo: Lerp this properly
        }


        currentXblendValue = Mathf.Clamp(currentXblendValue, -1f, 1f);
        currentYblendValue = Mathf.Clamp(currentYblendValue, -1f, 1f);

        _anim.SetFloat(AnimStringUtils.blendXHash, currentXblendValue);
        _anim.SetFloat(AnimStringUtils.blendYHash, currentYblendValue);

        //if (Mathf.Abs (currentYblendValue) > 0.1f && Mathf.Abs(currentXblendValue) > 0.1f)
        //{
        //    _anim.SetBool(AnimStringUtils.moveHash, false);
        //}
        //else
        //{
        //    _anim.SetBool(AnimStringUtils.moveHash, true);
        //}
 
    }
}
