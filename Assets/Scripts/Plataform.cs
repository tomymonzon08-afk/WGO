using UnityEngine;
using System.Collections;

public enum PlatformType
{
    Normal,
    Elimination,
    AbilityWhite,
    AbilityBlue,
    AbilityPurple,
    Launch,
    Teleport,
    Spawn
}

public class Platform : MonoBehaviour
{
    public PlatformType platformType;

    public Vector3 launchDirection = Vector3.forward;
    public float launchDistance = 2f; // 2 o 3 casillas

    private static readonly Color ColorNormal = new Color(0.91f, 0.90f, 0.87f);
    private static readonly Color ColorElimination = new Color(0.94f, 0.58f, 0.58f);
    private static readonly Color ColorWhite = new Color(0.56f, 0.93f, 0.56f);
    private static readonly Color ColorBlue = new Color(0.20f, 0.47f, 0.78f);
    private static readonly Color ColorPurple = new Color(0.69f, 0.66f, 0.93f);
    private static readonly Color ColorLaunch = new Color(0.98f, 0.78f, 0.46f);
    private static readonly Color ColorTeleport = new Color(0.53f, 0.81f, 0.98f); 
    private static readonly Color ColorSpawn = new Color(0.80f, 0.93f, 0.73f);

    private float eliminationTimer = 0f;
    private const float EliminationTime = 2f;

    private const float WhiteChargeTime = 2f;
    private const float BlueChargeTime = 3f;
    private const float PurpleChargeTime = 5f;

    private GameObject playerOnTop = null;

    private float abilityTimer = 0f;
    private bool abilityCharged = false;

    [Header("Teletransporte")]
    public float teleportCooldown = 5f;
    public bool OnCooldown { get; private set; } = false;
    private static readonly Color ColorTeleportCooldown = new Color(0.55f, 0.55f, 0.55f); // gris

    public void Initialize()
    {
        ApplyColor();
        if (platformType == PlatformType.Teleport && TeleportManager.Instance != null)
            TeleportManager.Instance.Register(this);
    }

    void ApplyColor()
    {
        Color color = platformType switch
        {
            PlatformType.Elimination => ColorElimination,
            PlatformType.AbilityWhite => ColorWhite,
            PlatformType.AbilityBlue => ColorBlue,
            PlatformType.AbilityPurple => ColorPurple,
            PlatformType.Launch => ColorLaunch,
            PlatformType.Teleport => ColorTeleport,
            PlatformType.Spawn => ColorSpawn,
            _ => ColorNormal,
        };

        GetComponent<Renderer>().material.color = color;
    }

    void OnCollisionEnter(Collision col)
    {
        if (!col.gameObject.CompareTag("Player")) return;

        playerOnTop = col.gameObject;
        eliminationTimer = 0f;
        abilityTimer = 0f;
        abilityCharged = false;

        if (platformType == PlatformType.Launch && col.gameObject.CompareTag("Player"))
        {
            LaunchPlayer(col.gameObject);
        }
        if (platformType == PlatformType.Teleport && col.gameObject.CompareTag("Player"))
        {
            TeleportPlayer(col.gameObject);
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject != playerOnTop) return;

        playerOnTop = null;
        eliminationTimer = 0f;
        abilityTimer = 0f;
        abilityCharged = false;
    }

    void Update()
    {
        // Lógica de eliminación
        if (platformType == PlatformType.Elimination && playerOnTop != null)
        {
            eliminationTimer += Time.deltaTime;
            if (eliminationTimer >= EliminationTime)
                EliminatePlayer(playerOnTop);
        }

        // Lógica de habilidad
        if (IsAbilityPlatform() && playerOnTop != null && !abilityCharged)
        {
            abilityTimer += Time.deltaTime;
            float required = GetRequiredChargeTime();

            if (abilityTimer >= required)
            {
                PlayerAbilities abilities = playerOnTop.GetComponent<PlayerAbilities>();
                if (abilities != null)
                {
                    abilities.UnlockAbility(platformType);
                    abilityCharged = true;
                }
            }
        }
    }

    void EliminatePlayer(GameObject player)
    {
        playerOnTop = null;
        eliminationTimer = 0f;
        GameManager.Instance.PlayerEliminated(player);
        player.SetActive(false);
    }
    float GetRequiredChargeTime()
    {
        return platformType switch
        {
            PlatformType.AbilityWhite => WhiteChargeTime,
            PlatformType.AbilityBlue => BlueChargeTime,
            PlatformType.AbilityPurple => PurpleChargeTime,
            _ => 0f
        };
    }

    bool IsAbilityPlatform()
    {
        return platformType == PlatformType.AbilityWhite ||
               platformType == PlatformType.AbilityBlue ||
               platformType == PlatformType.AbilityPurple;
    }
    void LaunchPlayer(GameObject player)
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb == null) return;

        PlayerMovement pm = player.GetComponent<PlayerMovement>();
        if (pm != null) pm.SetLaunched(true);

        rb.linearVelocity = Vector3.zero;
        Vector3 force = launchDirection.normalized * launchDistance * 10f;
        force.y = 5f;
        rb.AddForce(force, ForceMode.Impulse);

        StartCoroutine(ResetLaunch(pm));
    }

    IEnumerator ResetLaunch(PlayerMovement pm)
    {
        yield return new WaitForSeconds(0.8f); // tiempo bloqueado
        if (pm != null) pm.SetLaunched(false);
    }
    void TeleportPlayer(GameObject player)
    {
        if (TeleportManager.Instance == null) return;
        if (OnCooldown) return;

        Platform destination = TeleportManager.Instance.GetRandomDestination(this);
        if (destination == null)
        {
            Debug.Log("No hay destinos disponibles.");
            return;
        }

        // Teletransporta al jugador
        Vector3 targetPos = destination.transform.position;
        targetPos.y += 1f;
        player.transform.position = targetPos;

        Debug.Log($"{player.name} teletransportado a {destination.name}");

        // Activa cooldown en ambas casillas
        TeleportManager.Instance.ActivateCooldown(this, destination, teleportCooldown);
    }
    public void SetCooldown(bool active)
    {
        OnCooldown = active;
        GetComponent<Renderer>().material.color = active ? ColorTeleportCooldown : ColorTeleport;
    }
}