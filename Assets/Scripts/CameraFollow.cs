using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    [Header("Posición relativa al jugador")]
    public Vector3 offset = new Vector3(0, 8, -6);
    public Vector3 lookAtOffset = new Vector3(0, 0, 0);

    void LateUpdate()
    {
        if (target == null) return;
        transform.position = target.position + offset;
        transform.LookAt(target.position + lookAtOffset);
    }
}