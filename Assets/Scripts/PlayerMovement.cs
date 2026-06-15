using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed = 4f;
    public float runSpeed = 8f;
    public float jumpForce = 5f;

    [Header("Input")]
    public bool isPlayerTwo = false;

    private Rigidbody rb;

    [Header("Suelo")]
    public float groundCheckDistance = 0.4f;
    public LayerMask groundLayer;

    private Vector2 moveInput;
    private bool isRunning;

    private bool isLaunched = false;

    private bool isParalyzed = false;

    private PlayerAbilities abilities;

    private PlayerInputActions inputActions;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        abilities = GetComponent<PlayerAbilities>();
        inputActions = new PlayerInputActions();
    }

    void OnEnable()
    {
        if (isPlayerTwo)
        {
            inputActions.Player2.Enable();
            inputActions.Player2.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
            inputActions.Player2.Move.canceled += ctx => moveInput = Vector2.zero;
            inputActions.Player2.Run.performed += ctx => isRunning = true;
            inputActions.Player2.Run.canceled += ctx => isRunning = false;
            inputActions.Player2.Jump.performed += ctx => TryJump();
            inputActions.Player2.UseCommon.performed += ctx => abilities.UseCommon();
            inputActions.Player2.UseSpecial.performed += ctx => abilities.UseSpecial();
            inputActions.Player2.UseEpic.performed += ctx => abilities.UseEpic();
        }
        else
        {
            inputActions.Player.Enable();
            inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
            inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
            inputActions.Player.Run.performed += ctx => isRunning = true;
            inputActions.Player.Run.canceled += ctx => isRunning = false;
            inputActions.Player.Jump.performed += ctx => TryJump();
            inputActions.Player.UseCommon.performed += ctx => abilities.UseCommon();
            inputActions.Player.UseSpecial.performed += ctx => abilities.UseSpecial();
            inputActions.Player.UseEpic.performed += ctx => abilities.UseEpic();
        }
    }

    void OnDisable()
    {
        if (isPlayerTwo)
            inputActions.Player2.Disable();
        else
            inputActions.Player.Disable();
    }

    void FixedUpdate()
    {
        if (isLaunched || isParalyzed) return;

        float speed = isRunning ? runSpeed : walkSpeed;
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y).normalized * speed;

        if (move.sqrMagnitude > 0.01f)
        {
            rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);

            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 15f);
        }
    }

    void TryJump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + 0.1f, groundLayer);
    }
    public void SetLaunched(bool value)
    {
        isLaunched = value;
    }
    public void SetParalyzed(bool value)
    {
        isParalyzed = value;
        rb.linearVelocity = Vector3.zero;
    }
}