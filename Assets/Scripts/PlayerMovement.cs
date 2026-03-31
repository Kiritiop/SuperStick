using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public enum InputMode { Keyboard, Gamepad }

    [Header("Input Mode")]
    public InputMode inputMode = InputMode.Keyboard;

    [Header("Movement Settings")]
    public float moveSpeed = 6f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;

    [Header("Keyboard Keys")]
    public Key leftKey;
    public Key rightKey;
    public Key jumpKey;

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
        HandleGroundCheck();
        HandleMovement();
        HandleJump();


        // float x = transform.position.x;
        // float y = transform.position.y;
        // Debug.Log("Position: " + x + ", " + y);

        // Gamepad gamepad = Gamepad.current;
        // if (gamepad == null) return;
        // Vector2 leftStick = gamepad.leftStick.ReadValue();
        // Debug.Log(leftStick);
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
}