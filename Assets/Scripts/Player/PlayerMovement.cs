using System;
using Signals;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float JumpForce = 7f;
    private const float SideStepMultiplier = 0.25f;
    private float _speedMultiplier = 2f;
    private static readonly Vector3 StartPoint = new Vector3(-0.340319f, 0.271f, 0.340319f);
    private Vector3 _lastPosition = new Vector3(0,0,-1);

    [SerializeField] private Animator animator;
    [SerializeField] private PointsObject pointsObject;

    private CharacterController _controller;

    private float _sidestep;
    private float _currentJump;
    private float _heightBeforeJump;
    private bool _doubleJump;
    private int _deadMultiplier = 1;
    private bool _stopMultiplier = true;
    public int nextPoint = 5;

    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Right = Animator.StringToHash("Right");
    private static readonly int Left = Animator.StringToHash("Left");
    
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        pointsObject.ResetPoints();
    }
    
    void Update()
    {
        if (transform.position.z > nextPoint)
        {
            pointsObject.AddPoints(1);
            nextPoint++;
            //_speedMultiplier += 0.005f;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.P))
        {
            _stopMultiplier = !_stopMultiplier;
        }

        if (_stopMultiplier || _deadMultiplier == 0)
        {
            return;
        }
        
        Animations();
        // Check for Restart
        if (transform.position.y < _heightBeforeJump - 5.0f)
        {
            PlayerDead();
        }
    }

    public Vector3 PlayerPos()
    {
        var position = transform.position;
        return _controller.isGrounded ? position : new Vector3(position.x, _heightBeforeJump, position.z);
    }
    
    private void Animations()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
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
                    _currentJump = JumpForce;
                    _doubleJump = true;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!_controller.isGrounded)
            {
                _currentJump = -5;
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
        if (_stopMultiplier)
        {
            return;
        }
        Vector3 moveVector = new Vector3((_sidestep - transform.position.x) * SideStepMultiplier, _currentJump* Time.deltaTime, _deadMultiplier * _speedMultiplier* Time.deltaTime);
        _controller.Move(moveVector);
        if (Math.Abs(_lastPosition.z - transform.position.z) < 0.001)
        {
            PlayerDead();
        }

        _lastPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Pyramid obj = other.GetComponent<Pyramid>();
        if (obj == null)
        {
            return;
        }

        PlayerDead();
    }

    public void ResetPlayer()
    {
        _deadMultiplier = 1;
        _stopMultiplier = true;
        nextPoint = 5;
    }

    public void PlayerDead()
    {
        if (_deadMultiplier == 0)
        {
            return;
        }
        
        _deadMultiplier = 0;
        Supyrb.Signals.Get<PlayerDeadSignal>().Dispatch();
        //resultScreen.gameObject.SetActive(true);
        _controller.enabled = false;
        _controller.transform.position = StartPoint;
        _controller.enabled = true;
        _heightBeforeJump = transform.position.y;
        _sidestep = 0;
        _currentJump = 0;
    }
}