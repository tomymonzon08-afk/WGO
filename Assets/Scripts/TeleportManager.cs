using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public static TeleportManager Instance;

    private List<Platform> teleportPlatforms = new List<Platform>();

    void Awake()
    {
        Instance = this;
    }

    public void Register(Platform platform)
    {
        teleportPlatforms.Add(platform);
    }

    public Platform GetRandomDestination(Platform origin)
    {
        List<Platform> available = new List<Platform>();

        foreach (Platform p in teleportPlatforms)
        {
            if (p != origin && p.isActiveAndEnabled && !p.OnCooldown)
                available.Add(p);
        }

        if (available.Count == 0) return null;

        return available[Random.Range(0, available.Count)];
    }

    public void ActivateCooldown(Platform origin, Platform destination, float duration)
    {
        StartCoroutine(CooldownRoutine(origin, duration));
        StartCoroutine(CooldownRoutine(destination, duration));
    }

    private IEnumerator CooldownRoutine(Platform platform, float duration)
    {
        platform.SetCooldown(true);
        yield return new WaitForSeconds(duration);
        platform.SetCooldown(false);
    }
}