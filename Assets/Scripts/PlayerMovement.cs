using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public enum InputMode { Keyboard, Gamepad }

    [Header("Input Mode")]
    public InputMode inputMode = InputMode.Keyboard;
    
    [Header("Movement Settings")]
    public float moveSpeed = 6f;
    public float jumpForce = 100f;
    public string directionFacing;
    float jumpTimeCounter = 0f;
    float maxJumpTime = 0.5f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;
    private bool isJumping;

    [Header("Keyboard Keys")]
    public Key leftKey;
    public Key rightKey;
    public Key jumpKey;
    public Key shootKey;

    [Header("Player Color")]
    public Color playerColor = Color.white;

    [SerializeField] private AudioClip JumpSFX;
    private Rigidbody2D rb;
    private SpriteRenderer sr;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.color = playerColor;
    }

    void Update()
    {
        HandleDirection();
        HandleGroundCheck();
        HandleMovement();
        HandleJump();

        // Gamepad gamepad = Gamepad.current;
        // if (gamepad == null) return;
        // Vector2 leftStick = gamepad.leftStick.ReadValue();
        // Debug.Log(leftStick);
    }

    void HandleDirection()
    {
        if (Keyboard.current[rightKey].isPressed)
        {
            directionFacing = "right";
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Keyboard.current[leftKey].isPressed)
        {
            directionFacing = "left";
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
    }

    // void OnGUI()
    // {
    //     GUI.Label(new Rect(10, 10, 300, 20), 
    //         $"Position: {transform.position.x:F2}, {transform.position.y:F2}");
    // }

    void HandleGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    void HandleMovement()
    {
        float moveInput = 0f;

        if (inputMode == InputMode.Keyboard)
        {
            if (Keyboard.current[rightKey].isPressed)
                moveInput = 1f;

            else if (Keyboard.current[leftKey].isPressed)
                moveInput = -1f;
        }
    
        else if (inputMode == InputMode.Gamepad)
        {
            if (Gamepad.current != null)
            {
                float stickX = Gamepad.current.leftStick.ReadValue().x;
                if (Mathf.Abs(stickX) > 0.1f)
                moveInput = stickX;
            }
        }

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void HandleJump()
    {
        bool jumpPressed = false;

        if (inputMode == InputMode.Keyboard)
        {
            jumpPressed = Keyboard.current[jumpKey].wasPressedThisFrame;
        }
        
        else if (inputMode == InputMode.Gamepad)
        {
            jumpPressed = Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame;
        }
        
        if (jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            SoundEffectManager.instance.PlaySoundEffect(JumpSFX, transform, 0.5f);
        }
    }

    public bool IsGrounded() => isGrounded;

    public string Direction() => directionFacing;
}