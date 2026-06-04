using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPush : MonoBehaviour
{
    [Header("Empuje")]
    public float pushForce = 12f;
    public float pushRange = 1.5f;
    public LayerMask playerLayer;

    private PlayerInputActions inputActions;

    void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    void OnEnable()
    {
        inputActions.Players.Enable();
        inputActions.Players.Push.performed += ctx => TryPush();
    }

    void OnDisable()
    {
        inputActions.Players.Disable();
    }

    void TryPush()
    {
        // Busca un jugador en el rango frontal
        Collider[] hits = Physics.OverlapSphere(
            transform.position + transform.forward * pushRange,
            0.6f,
            playerLayer
        );

        foreach (Collider hit in hits)
        {
            if (hit.gameObject == gameObject) continue; // no se empuja a sí mismo

            Rigidbody targetRb = hit.GetComponent<Rigidbody>();
            if (targetRb == null) continue;

            Vector3 direction = (hit.transform.position - transform.position).normalized;
            direction.y = 0; // empuje horizontal, sin componente vertical
            targetRb.AddForce(direction * pushForce, ForceMode.Impulse);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Muestra el rango de empuje en la Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * pushRange, 0.6f);
    }
}