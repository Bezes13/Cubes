using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float JumpForce = 7f;
    private const float SideStepMultiplier = 0.25f;
    private const float SpeedMultiplier = 2f;
    private static readonly Vector3 StartPoint = new Vector3(-0.340319f, 0.271f, 0.340319f);

    [SerializeField] private Animator animator;
    [SerializeField] private ResultScreen _resultScreen;

    private CharacterController _controller;

    private float _sidestep;
    private float _currentJump;
    private float _heightBeforeJump;
    private bool _doubleJump;
    private int _deadMultiplier = 1;
    private bool _stopMultiplier = true;

    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Right = Animator.StringToHash("Right");
    private static readonly int Left = Animator.StringToHash("Left");
    
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.P))
        {
            _stopMultiplier = !_stopMultiplier;
        }

        if (_stopMultiplier)
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
        if (_stopMultiplier)
        {
            return;
        }
        Vector3 moveVector = new Vector3((_sidestep - transform.position.x) * SideStepMultiplier, _currentJump* Time.deltaTime, _deadMultiplier * SpeedMultiplier* Time.deltaTime);
        _controller.Move(moveVector);
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
    }

    public void PlayerDead()
    {
        if (_deadMultiplier == 0)
        {
            return;
        }
        
        _deadMultiplier = 0;
        _resultScreen.gameObject.SetActive(true);
        _controller.enabled = false;
        _controller.transform.position = StartPoint;
        _controller.enabled = true;
        _heightBeforeJump = transform.position.y;
        _sidestep = 0;
    }
}