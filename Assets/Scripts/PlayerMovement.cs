using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 6f;
    public float jumpForce = 3f;
    public string directionFacing;
    float jumpTimeCounter = 0f;
    float maxJumpTime = 0.5f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;
    private bool isJumping;

    [Header("Input Keys")]
    public Key leftKey;
    public Key rightKey;
    public Key jumpKey;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        HandleDirection();
        HandleGroundCheck();
        HandleMovement();
        HandleJump();
    }

    void HandleDirection()
    {
        if (Keyboard.current[rightKey].isPressed)
        {
            directionFacing = "right";
        }
        else if (Keyboard.current[leftKey].isPressed)
        {
            directionFacing = "left";
        }
        
    }

    void HandleGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    void HandleMovement()
    {
        if (Keyboard.current[rightKey].isPressed)
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        else if (Keyboard.current[leftKey].isPressed)
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        
    }

    void HandleJump()
    {

        if (Keyboard.current[jumpKey].wasPressedThisFrame && isGrounded)
        {
            //rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce/10);
            isJumping = true;
            jumpTimeCounter = 0;
        }
        if (Keyboard.current[jumpKey].isPressed && jumpTimeCounter <= maxJumpTime && isJumping)
        {
            jumpTimeCounter += Time.deltaTime;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * (Mathf.Sqrt(jumpTimeCounter)));
        }
        else if (Keyboard.current[jumpKey].isPressed && jumpTimeCounter > maxJumpTime)
        {
            isJumping = false;
            
        }
  
    }

    public bool IsGrounded() => isGrounded;

    public string Direction() => directionFacing;
}