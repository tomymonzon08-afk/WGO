using UnityEngine;
using System.Collections;

public class PlayerVisuals : MonoBehaviour
{
    private Renderer playerRenderer;
    private Color originalColor;

    private static readonly Color ColorImmune = new Color(0.0f, 0.8f, 1.0f); // cyan
    private static readonly Color ColorParalyzed = new Color(1.0f, 0.9f, 0.0f); // amarillo
    private static readonly Color ColorSpeed = new Color(1.0f, 0.4f, 0.0f); // naranja

    private PlayerAbilities abilities;
    private PlayerMovement movement;

    void Start()
    {
        playerRenderer = GetComponentInChildren<Renderer>();
        originalColor = playerRenderer.material.color;
        abilities = GetComponent<PlayerAbilities>();
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (movement.IsParalyzed)
            SetColor(ColorParalyzed);
        else if (abilities.IsImmune)
            SetColor(ColorImmune);
        else if (abilities.IsSpeedBoosted)
            SetColor(ColorSpeed);
        else
            SetColor(originalColor);
    }

    void SetColor(Color color)
    {
        playerRenderer.material.color = color;
    }

    public IEnumerator FlashTeleport()
    {
        Color flashColor = new Color(0.8f, 1.0f, 1.0f); // cyan blanco
        float duration = 0.4f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = Mathf.PingPong(elapsed * 8f, 1f);
            SetColor(Color.Lerp(originalColor, flashColor, t));
            elapsed += Time.deltaTime;
            yield return null;
        }

        SetColor(originalColor);
    }

}