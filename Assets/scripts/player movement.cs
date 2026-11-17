

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInput playerInput;

    public float speed = 5f;
    public float jumpForce = 8f;

    public Transform groundChecker;
    public float groundDistance = 0.15f;
    public LayerMask groundLayer;

    public int maxJumps = 2;
    private int jumpsRemaining;

    private Vector2 movement;

    private bool isGrounded;
    private bool jumpPressed;
    SpriteRenderer sr;
    private object movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        sr = GetComponent<SpriteRenderer>();

        jumpsRemaining = maxJumps;
    }

    void Update()
    {
        // INPUT DE MOVIMIENTO
        movement = playerInput.actions["Move"].ReadValue<Vector2>();

        // INPUT DE SALTO
        if (playerInput.actions["Jump"].WasPressedThisFrame())
        {
            jumpPressed = true;
        }

        // COMPROBAR SUELO
        isGrounded = Physics2D.Raycast(
            groundChecker.position,
            Vector2.down,
            groundDistance,
            groundLayer
        );

        if (isGrounded && rb.linearVelocity.y <= 0.01f)
        {
            jumpsRemaining = maxJumps;
        }

        // SPRITE FLIP
        if (movement.x != 0f)
        {
            if (movement.x > 0f)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
        }
    }

    void FixedUpdate()
    {
        // MOVIMIENTO HORIZONTAL
        rb.linearVelocity = new Vector2(
            movement.x * speed,
            rb.linearVelocity.y
        );

        // SALTO Y DOBLE SALTO
        if (jumpPressed)
        {
            if (jumpsRemaining > 0)
            {
                rb.linearVelocity = new Vector2(
                    rb.linearVelocity.x,
                    jumpForce
                );

                jumpsRemaining--;
            }

            jumpPressed = false;
        }
    }
}
