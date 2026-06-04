using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [Header("Estado de habilidades")]
    public bool commonReady = false;
    public bool specialReady = false;
    public bool epicReady = false;

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
}