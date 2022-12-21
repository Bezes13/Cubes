using System;
using DefaultNamespace;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float JumpForce = 7f;
    private const float SideStepMultiplier = 0.25f;
    private const float SpeedMultiplier = 2f;
    
    [SerializeField] private Animator animator;

    private CharacterController _controller;

    private float _sidestep;
    private float _currentJump;
    private float _heightBeforeJump;
    private bool _doubleJump;

    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Right = Animator.StringToHash("Right");
    private static readonly int Left = Animator.StringToHash("Left");
    
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        Animations();
    }

    public Vector3 PlayerPos()
    {
        var position = transform.position;
        return _controller.isGrounded ? position : new Vector3(position.x, _heightBeforeJump, position.z);
    }
    
    
    private void Animations()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_controller.isGrounded)
            {
                _heightBeforeJump = transform.position.y;
                animator.SetTrigger(Jump);
                _currentJump = JumpForce;
                _doubleJump = false;
            }
            else
            {
                if (!_doubleJump)
                {
                    animator.SetTrigger(Jump);
                    _currentJump += JumpForce;
                    _doubleJump = true;
                }
            }
        }

        if (!_controller.isGrounded)
        {
            _currentJump -= JumpForce * 2 * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            animator.SetTrigger(Left);
            _sidestep = _sidestep != transform.position.x ? _sidestep - 1 : transform.position.x - 1f;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            animator.SetTrigger(Right);
            _sidestep = _sidestep != transform.position.x ? _sidestep + 1 : transform.position.x + 1f;
        }
    }

    private void FixedUpdate()
    {
        Vector3 moveVector = new Vector3((_sidestep - transform.position.x) * SideStepMultiplier, _currentJump* Time.deltaTime, SpeedMultiplier* Time.deltaTime);
        _controller.Move(moveVector);
    }

    private void OnTriggerEnter(Collider other)
    {
        Pyramid obj = other.GetComponent<Pyramid>();
        if (obj == null)
        {
            return;
        }
        Destroy(gameObject);
    }
}