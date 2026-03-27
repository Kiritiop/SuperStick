using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 6f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;

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
        HandleGroundCheck();
        HandleMovement();
        HandleJump();
        float x = transform.position.x;
        float y = transform.position.y;
        Debug.Log("Position: " + x + ", " + y);
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
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    public bool IsGrounded() => isGrounded;
}