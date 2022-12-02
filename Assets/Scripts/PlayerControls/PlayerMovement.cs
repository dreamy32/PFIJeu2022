using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [Header("Physics")]
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float gravity = -9.81f; // The numerical value for the acceleration of gravity
    [SerializeField] float jumpForce = 10f;


    [Header("Sounds")]
    [SerializeField] SoundManager soundManager;
    [SerializeField] AudioClip footsteps;
    AudioSource soundSource;

    [Header("Crouching")]
    [SerializeField] float crouchingHeight = 0.5f;
    [SerializeField] Transform topRaycastLocation;


    private float speed;
    private float defaultSpeed;
    private float defaultGravity;
    private float defaultHeight;

    bool isRunning = false;
    bool isGrounded = true;
    bool isMoving = false;
    private bool isCrouching = false;

    private GameObject eyes;
    private Animator camAnim;
    private CharacterController controller;
    private Vector3 velocity;
    

    void Awake()
    {
        soundSource = GetComponent<AudioSource>();
        speed = walkSpeed;
        controller = GetComponent<CharacterController>();
        defaultHeight = controller.height;
        eyes = transform.GetChild(0).gameObject;
        camAnim = eyes.GetComponent<Animator>();
    }
    private void Update()
    {
        isGrounded = controller.isGrounded;
    }


    public void Move(Vector2 input)
    {
        if (input != new Vector2(0, 0))
        {
            soundSource.pitch = isRunning ? 1.18f : 1;
            isMoving = true;
            camAnim.SetBool("isWalking", true);
            soundSource.enabled = true;
            
        } 
        else
        {
            
            camAnim.SetBool("isWalking", false);
            camAnim.SetBool("isIdle", true);
            soundSource.enabled = false ;
        }
          



        Vector3 direction = new Vector3(input.x,  0, input.y);
        controller.Move(transform.TransformDirection(direction) * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;


        // The player will jump or fall, depending on the value of the velocity
        controller.Move(velocity * Time.deltaTime);
    }
    public void Sprint(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            isRunning = true;
        else if (ctx.canceled)
            isRunning = false;
        speed = isRunning ? sprintSpeed : walkSpeed;
    }

    // An event that is invoked when pressing space
    public void Jump(bool fromJumpPad = false)
    {
        if (isGrounded)
        {
            this.velocity.y = jumpForce;
        }
    }

    public void Crouch()
    {
       
            if (!isCrouching && controller.isGrounded) // if standing up
            {
                CrouchMovement();
            }
            else // if crouching
            {
                controller.height = defaultHeight;
                isCrouching = false;
            }
        
    }

    private void CrouchMovement()
    {
        controller.height = crouchingHeight;
        isCrouching = true;
    }
    private bool CanStandUp()
    {
        //return !rayCast.ObjRay.
        return !Physics.Raycast(topRaycastLocation.position, topRaycastLocation.up, out RaycastHit hit, 2);
    }


}