using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [Header("Physics")] [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float gravity = -9.81f; // The numerical value for the acceleration of gravity

    [Header("Crouching")] [SerializeField] private float crouchingHeight = 0.5f;
    [SerializeField] private Transform topRaycastLocation;


    private float _speed;
    private float _defaultSpeed;
    private float _defaultGravity;
    private float _defaultHeight;
    private Vector3 _velocity;

    private bool _isRunning = false;
    private bool _isGrounded = true;
    private bool _isCrouching = false;

    private CharacterController _controller;
    private GameObject _eyes;
    private Animator _camAnim;
    private AudioSource _soundSource;
    
    //Animator parameters
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsIdle = Animator.StringToHash("isIdle");


    private void Awake()
    {
        _soundSource = GetComponent<AudioSource>();
        _speed = walkSpeed;
        _controller = GetComponent<CharacterController>();
        _defaultHeight = _controller.height;
        _eyes = transform.GetChild(0).gameObject;
        _camAnim = _eyes.GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isGrounded != _controller.isGrounded)
            _isGrounded = _controller.isGrounded;
    }


    public void Move(Vector2 input)
    {
        if (input != new Vector2(0, 0))
        {
            _soundSource.pitch = _isRunning ? 1.18f : 1;
            _camAnim.SetBool(IsWalking, true);
            _soundSource.enabled = true;
        }
        else
        {
            _camAnim.SetBool(IsWalking, false);
            _camAnim.SetBool(IsIdle, true);
            _soundSource.enabled = false;
        }


        Vector3 direction = new Vector3(input.x, 0, input.y);
        _controller.Move(transform.TransformDirection(direction) * (_speed * Time.deltaTime));
        _velocity.y += gravity * Time.deltaTime;

        if (_isGrounded && _velocity.y < 0)
            _velocity.y = -2f;

        // The player will jump or fall, depending on the value of the velocity
        _controller.Move(_velocity * Time.deltaTime);
    }

    public void Sprint(InputAction.CallbackContext ctx)
    {
        if (!_isCrouching)
        {
            if (ctx.performed)
                _isRunning = true;

            else if (ctx.canceled)
                _isRunning = false;
            _speed = _isRunning ? sprintSpeed : walkSpeed;
        }
    }

    public void Crouch(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (!_isCrouching && _controller.isGrounded && !_isRunning) // if standing up
            {
                CrouchMovement();
            }
            else // if crouching
            {
                _controller.height = _defaultHeight;
                _isCrouching = false;
            }
        }
    }

    private void CrouchMovement()
    {
        _controller.height = crouchingHeight;
        _isCrouching = true;
    }

    private bool CanStandUp()
    {
        return !Physics.Raycast(topRaycastLocation.position, topRaycastLocation.up, out RaycastHit hit, 2);
    }
}