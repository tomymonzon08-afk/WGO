using System.Collections;
using UnityEngine;

public enum CharacterType
{
    Arashiko,
    MariaElena,
    Jeffrey
}

public class PlayerAbilities : MonoBehaviour
{
    [Header("Personaje")]
    public CharacterType characterType;

    [Header("Estado de habilidades")]
    public bool commonReady = false;
    public bool specialReady = false;
    public bool epicReady = false;

    private PlayerMovement movement;
    private PlayerPush push;

    public bool IsSpeedBoosted { get; private set; } = false;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        push = GetComponent<PlayerPush>();
    }

    public void UnlockAbility(PlatformType type)
    {
        switch (type)
        {
            case PlatformType.AbilityWhite:
                commonReady = true;
                Debug.Log($"{gameObject.name}: habilidad común lista.");
                break;
            case PlatformType.AbilityBlue:
                specialReady = true;
                Debug.Log($"{gameObject.name}: habilidad especial lista.");
                break;
            case PlatformType.AbilityPurple:
                epicReady = true;
                Debug.Log($"{gameObject.name}: habilidad épica lista.");
                break;
        }
    }

    // --- HABILIDAD COMÚN ---
    public void UseCommon()
    {
        if (!commonReady) return;
        commonReady = false;
        StartCoroutine(CommonRoutine());
        Debug.Log($"{gameObject.name}: habilidad común activada.");
    }

    IEnumerator CommonRoutine()
    {
        float original = push.pushForce;
        push.pushForce *= 2f;
        yield return new WaitForSeconds(6f);
        push.pushForce = original;
        Debug.Log($"{gameObject.name}: habilidad común terminada.");
    }

    // --- HABILIDAD ESPECIAL ---
    public void UseSpecial()
    {
        if (!specialReady) return;
        specialReady = false;
        StartCoroutine(SpecialRoutine());
        Debug.Log($"{gameObject.name}: habilidad especial activada.");
    }

    IEnumerator SpecialRoutine()
    {
        SetImmune(true);
        yield return new WaitForSeconds(3f);
        SetImmune(false);
        Debug.Log($"{gameObject.name}: inmunidad terminada.");
    }

    public bool IsImmune { get; private set; } = false;

    void SetImmune(bool value)
    {
        IsImmune = value;
    }

    // --- HABILIDAD ÉPICA ---
    public void UseEpic()
    {
        if (!epicReady) return;
        epicReady = false;

        switch (characterType)
        {
            case CharacterType.Arashiko: StartCoroutine(EpicArashiko()); break;
            case CharacterType.MariaElena: EpicMariaElena(); break;
            case CharacterType.Jeffrey: StartCoroutine(EpicJeffrey()); break;
        }

        Debug.Log($"{gameObject.name}: habilidad épica activada.");
    }

    // Arashiko — paraliza enemigos en 3x3 casillas durante 5 seg
    IEnumerator EpicArashiko()
    {
        float radius = 3f * 2.05f / 2f; // 1.5 casillas en unidades de mundo
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hit in hits)
        {
            if (hit.gameObject == gameObject) continue;
            PlayerMovement pm = hit.GetComponent<PlayerMovement>();
            PlayerAbilities pa = hit.GetComponent<PlayerAbilities>();
            if (pm == null) continue;
            if (pa != null && pa.IsImmune) continue; // respeta inmunidad
            pm.SetParalyzed(true);
        }

        yield return new WaitForSeconds(5f);

        Collider[] hits2 = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hit in hits2)
        {
            if (hit.gameObject == gameObject) continue;
            PlayerMovement pm = hit.GetComponent<PlayerMovement>();
            if (pm != null) pm.SetParalyzed(false);
        }
    }

    // MariaElena — teletransporte 4 casillas adelante
    void EpicMariaElena()
    {
        Vector3 destination = transform.position + transform.forward * (4f * 2.05f);
        // Mantiene la altura actual
        destination.y = transform.position.y;
        transform.position = destination;
    }

    // Jeffrey — triplica velocidad de caminata por 10 seg
    IEnumerator EpicJeffrey()
    {
        float original = movement.walkSpeed;
        movement.walkSpeed *= 3f;
        IsSpeedBoosted = true;
        yield return new WaitForSeconds(10f);
        movement.walkSpeed = original;
        IsSpeedBoosted = false;
    }
}