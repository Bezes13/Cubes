using System;
using Model;
using Objects;
using Signals;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private const float JumpForce = 7f;
        private const float SideStepMultiplier = 0.25f;

        [SerializeField] private Animator animator;
        [SerializeField] private PointsObject pointsObject;
        [SerializeField] private PathModel model;
        [SerializeField] private ParticleSystem starExplosion;

        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip deadSound;
        [SerializeField] private AudioClip collectSound;
        [SerializeField] private AudioClip jumpSound;
        [SerializeField] private AudioClip dashSound;
        private Vector3 _lastPosition = new Vector3(0, 0, -1);
        private CharacterController _controller;
        private float _sidestep = -1;
        private float _currentJump;
        private float _heightBeforeJump;
        private float _speedMultiplier = 2f;
        private int _deadMultiplier = 1;
        private int _nextPoint = 5;
        private bool _stopMultiplier = true;
        private bool _doubleJump;
        private Vector2 startTouchPos;
        private Vector2 endTouchPos;

        private static readonly Vector3 StartPoint = new Vector3(0f, 1.271f, 0.340319f);
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Right = Animator.StringToHash("Right");
        private static readonly int Left = Animator.StringToHash("Left");
        private static readonly int Move = Animator.StringToHash("Move");

        private void Awake()
        {
            Supyrb.Signals.Get<StartGameSignal>().AddListener(StartGame);
            Supyrb.Signals.Get<PauseSignal>().AddListener(Pause);
            Supyrb.Signals.Get<UnPauseSignal>().AddListener(Pause);
        }

        private void StartGame()
        {
            _stopMultiplier = !_stopMultiplier;
        }

        int TouchInput()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startTouchPos = Input.GetTouch(0).position;
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                endTouchPos = Input.GetTouch(0).position;
                var x = endTouchPos.x - startTouchPos.x;
                var y = endTouchPos.y - startTouchPos.y;

                if (Math.Abs(x) > Math.Abs(y))
                {
                    // swipe left or right
                    return x > 0 ? 1 : -1;
                }

                return y > 0 ? 3 : 4;
            }

            return 0;
        }

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            pointsObject.ResetPoints();
        }

        private void Pause()
        {
            _stopMultiplier = !_stopMultiplier;
        }

        private void Update()
        {
            if (_controller.isGrounded) animator.SetBool(Jump, false);
            animator.SetBool(Move, !_stopMultiplier && _deadMultiplier != 0);
            if (transform.position.z > _nextPoint && _speedMultiplier <= 3f)
            {
                _nextPoint += 5;
                _speedMultiplier += 0.01f;
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                _stopMultiplier = !_stopMultiplier;
            }

            if (_stopMultiplier || _deadMultiplier == 0)
            {
                return;
            }

            Movement();
            // Check for Restart
            if (transform.position.y < _heightBeforeJump - 5.0f)
            {
                PlayerDead();
            }
        }

        public Vector3 PlayerPos()
        {
            return transform.position;
        }

        private void Movement()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) ||
                TouchInput() == 3)
            {
                if (_controller.isGrounded)
                {
                    _heightBeforeJump = transform.position.y;
                    animator.SetBool(Jump, true);
                    _currentJump = JumpForce;
                    _doubleJump = false;
                    audioSource.clip = jumpSound;
                    audioSource.Play();
                }
                else
                {
                    if (!_doubleJump)
                    {
                        animator.SetBool(Jump, true);
                        _currentJump = JumpForce;
                        _doubleJump = true;
                        audioSource.clip = jumpSound;
                        audioSource.Play();
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || TouchInput() == 4)
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

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || TouchInput() == -1)
            {
                animator.SetBool(Left, true);
                _sidestep = _sidestep != transform.position.x ? _sidestep - 1 : transform.position.x - 1f;
                audioSource.clip = dashSound;
                audioSource.Play();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || TouchInput() == 1)
            {
                animator.SetBool(Right, true);
                _sidestep = _sidestep != transform.position.x ? _sidestep + 1 : transform.position.x + 1f;
                audioSource.clip = dashSound;
                audioSource.Play();
            }
        }

        private void FixedUpdate()
        {
            if (_stopMultiplier)
            {
                return;
            }

            Vector3 moveVector = new Vector3((_sidestep - transform.position.x) * SideStepMultiplier,
                _currentJump * Time.deltaTime, _deadMultiplier * _speedMultiplier * Time.deltaTime);
            _controller.Move(moveVector);
            if (Math.Abs(moveVector.x) <= 0.1)
            {
                animator.SetBool(Right, false);
                animator.SetBool(Left, false);
            }

            if (Math.Abs(_lastPosition.z - transform.position.z) < 0.001)
            {
                PlayerDead();
            }

            _lastPosition = transform.position;
            if (_stopMultiplier || _deadMultiplier == 0)
            {
                return;
            }

            pointsObject.AddPoints(1);
        }

        private void OnTriggerEnter(Collider other)
        {
            var obj = other.GetComponent<Pyramid>();
            if (obj != null)
            {
                PlayerDead();
            }

            var star = other.GetComponent<CollectableCoin>();
            if (star == null)
            {
                return;
            }
            
            starExplosion.gameObject.SetActive(true);
            starExplosion.Play();
            audioSource.clip = collectSound;
            audioSource.Play();
            Destroy(other.gameObject);
        }

        public void ResetPlayer()
        {
            _deadMultiplier = 1;
            _stopMultiplier = true;
            _speedMultiplier = 2f;
            _nextPoint = 5;
        }

        private void PlayerDead()
        {
            if (_deadMultiplier == 0)
            {
                return;
            }

            animator.SetBool(Move, false);
            _deadMultiplier = 0;
            Supyrb.Signals.Get<PlayerDeadSignal>().Dispatch();
            audioSource.clip = deadSound;
            audioSource.Play();
            _controller.enabled = false;
            _controller.transform.position = StartPoint;
            _controller.enabled = true;
            _heightBeforeJump = transform.position.y;
            _sidestep = 0;
            _currentJump = 0;
        }
    }
}