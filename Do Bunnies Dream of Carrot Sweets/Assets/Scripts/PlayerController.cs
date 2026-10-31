using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 12f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckradius = .2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Better Jump")]
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rigidBody;
    private bool isGrounded;
    private Animator playerAnimator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        playerControls.Movement.Jump.performed += OnJump;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        PlayerInput();
        CheckIsGrounded();
        HandleVariableJump();
        UpdateAnimations();
        FlipSprite();
    }

    private void FixedUpdate()
    {
        Move();
        HandleBetterFall();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
    }

    private void CheckIsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckradius, groundLayer);
    }

    private void Move()
    {
        rigidBody.linearVelocity = new Vector2(movement.x * moveSpeed, rigidBody.linearVelocity.y);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, jumpForce);
        }
    }

    private void HandleBetterFall()
    {
        if (rigidBody.linearVelocity.y < 0)
        {
            rigidBody.linearVelocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.fixedDeltaTime;
        }
    }

    private void HandleVariableJump()
    {
        bool releasedJumpEarly = rigidBody.linearVelocity.y > 0 && !playerControls.Movement.Jump.IsPressed();

        if (releasedJumpEarly)
        {
            rigidBody.linearVelocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
        }
    }

    private void UpdateAnimations()
    {
        playerAnimator.SetFloat("moveX", movement.x);
        playerAnimator.SetBool("isGrounded", isGrounded);
    }

    private void FlipSprite()
    {
        if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
