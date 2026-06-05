using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPush : MonoBehaviour
{
    [Header("Empuje")]
    public float pushForce = 12f;
    public float pushRange = 3f;
    public LayerMask playerLayer;

    [Header("Input")]
    public bool isPlayerTwo = false;

    private PlayerInputActions inputActions;

    void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    void OnEnable()
    {
        if (isPlayerTwo)
        {
            inputActions.Player2.Enable();
            inputActions.Player2.Push.performed += ctx => TryPush();
        }
        else
        {
            inputActions.Player.Enable();
            inputActions.Player.Push.performed += ctx => TryPush();
        }
    }

    void OnDisable()
    {
        if (isPlayerTwo)
            inputActions.Player2.Disable();
        else
            inputActions.Player.Disable();
    }

    void TryPush()
    {
        Collider[] hits = Physics.OverlapSphere(
            transform.position + transform.forward * pushRange,
            1.2f,
            playerLayer
        );

        foreach (Collider hit in hits)
        {
            if (hit.gameObject == gameObject) continue;

            Rigidbody targetRb = hit.GetComponent<Rigidbody>();
            if (targetRb == null) continue;

            Vector3 direction = (hit.transform.position - transform.position).normalized;
            direction.y = 0;
            targetRb.AddForce(direction * pushForce, ForceMode.Impulse);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * pushRange, 1.2f);
    }
}