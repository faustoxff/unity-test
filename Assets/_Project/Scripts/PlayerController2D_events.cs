using UnityEngine;
using UnityEngine.InputSystem;


[RequiredComponent(typeof(RigidBody2D))]

public class PlayerController2D_events : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float moveSpeed = 5f;

    [Header("Salto")]

    public float jumpForce = 7f;

    public Transform groundCheck;

    public float groundCheckRadius = 0.2f;

    public LayerMask whatIsGround;

    private Rigidbody2D rb;

    private float moveInput;

    private bool isGrounded;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<float>();

        if (ctx.canceled) moveInput = 0f;
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (!groundCheck) return;

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
