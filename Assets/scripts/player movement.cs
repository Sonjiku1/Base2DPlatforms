
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]

public class playermovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInput playerInput;

    public float velocidad = 5f;
    public float fuerzaSalto = 8f;

    public Transform detectorSuelo;
    public float distanciaSuelo = 0.15f;
    public LayerMask suelo;

    private Vector2 movimiento;
    private bool estaEnSuelo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        // Movimiento WASD
        movimiento = playerInput.actions["Move"].ReadValue<Vector2>();

        // Detectar el suelo con Raycast
        estaEnSuelo = Physics2D.Raycast(
            detectorSuelo.position,
            Vector2.down,
            distanciaSuelo,
            suelo
        );

        // Saltar todas las veces que quieras
        if (Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                fuerzaSalto
            );
        }
    }

    void FixedUpdate()
    {
        // Movimiento horizontal
        rb.linearVelocity = new Vector2(
            movimiento.x * velocidad,
            rb.linearVelocity.y
        );
    }

    // Dibujar el Raycast
    void OnDrawGizmos()
    {
        if (detectorSuelo != null)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawLine(
                detectorSuelo.position,
                detectorSuelo.position + Vector3.down * distanciaSuelo
            );
        }
    }
}
