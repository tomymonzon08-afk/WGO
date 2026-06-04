using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed = 4f;
    public float runSpeed = 8f;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private bool isGrounded;

    private Vector2 moveInput;
    private bool isRunning;

    private PlayerInputActions inputActions;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputActions = new PlayerInputActions();
    }

    void OnEnable()
    {
        inputActions.Players.Enable();

        inputActions.Players.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Players.Move.canceled += ctx => moveInput = Vector2.zero;

        inputActions.Players.Run.performed += ctx => isRunning = true;
        inputActions.Players.Run.canceled += ctx => isRunning = false;

        inputActions.Players.Jump.performed += ctx => TryJump();
    }

    void OnDisable()
    {
        inputActions.Players.Disable();
    }

    void FixedUpdate()
    {
        float speed = isRunning ? runSpeed : walkSpeed;
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y).normalized * speed;
        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);

        // Rota el personaje hacia donde se mueve
        if (move.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 15f);
        }
    }

    void TryJump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Plataform"))
            isGrounded = true;
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Plataform"))
            isGrounded = false;
    }
}