
using UnityEngine;
using UnityEngine.InputSystem;

public class playermovement : MonoBehaviour
{
    PlayerInput playerInput;
    Rigidbody rb;

    public float velocidad = 5f;
    private Vector2 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        movementInput = playerInput.actions["Move"].ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(
            movementInput.x,
            0f,
            movementInput.y
        );

        rb.linearVelocity = new Vector3(
            movement.x * velocidad,
            rb.linearVelocity.y,
            movement.z * velocidad
        );
    }
}
