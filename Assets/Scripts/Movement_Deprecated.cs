using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody _rigidbody;
    
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    
    public float playerHeight;
    public LayerMask whatIsGround;
    public Transform orientation;
    private bool grounded;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private GameObject lastHit;
    private bool readyToJump;

    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Right = Animator.StringToHash("Right");
    private static readonly int Left = Animator.StringToHash("Left");
    private Vector3 _velocity;

    // Start is called before the first frame update
    void Start()
    {
        readyToJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, out var hit);
        handleRaycast(hit);

        if (grounded)
            _rigidbody.drag = groundDrag;
        else
            _rigidbody.drag = 0;
        Animations();
        MovePlayer();
    }
    
    private void FixedUpdate()
    {
        
    }

    private void handleRaycast(RaycastHit hit)
    {
        if (grounded)
        {
            var newHit = hit.collider.gameObject;
            if (newHit != lastHit)
            {
                newHit.gameObject.SendMessage("ResetColor");
            }

            lastHit = newHit;
            lastHit.SendMessage("ChangeColor");
        }
        else
        {
            lastHit.SendMessage("ResetColor");
        }
    }

    private void Animations()
    {
        if (Input.GetKeyDown(KeyCode.Space) && readyToJump && grounded)
        {
            animator.SetTrigger(Jump);
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);

            _rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            animator.SetTrigger(Left);
            var transformPosition = transform.position;
            transformPosition.x -= 1;
            transform.position = transformPosition;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            animator.SetTrigger(Right);
            var transformPosition = transform.position;
            transformPosition.x += 1;
            transform.position = transformPosition;
        }
    }
    
    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward;

        // on ground
        if(grounded)
            _rigidbody.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if(!grounded)
            _rigidbody.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}