using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    [Header("Íconos")]
    public Image commonIcon;
    public Image specialIcon;
    public Image epicIcon;

    [Header("Jugador")]
    public PlayerAbilities playerAbilities;

    private static readonly Color ColorCommonReady = new Color(0.20f, 0.80f, 0.20f); // verde
    private static readonly Color ColorSpecialReady = new Color(0.10f, 0.30f, 0.80f); // azul oscuro
    private static readonly Color ColorEpicReady = new Color(0.60f, 0.30f, 0.90f); // violeta
    private static readonly Color ColorNotReady = new Color(0.25f, 0.25f, 0.25f); // gris

    void Update()
    {
        if (playerAbilities == null) return;

        commonIcon.color = playerAbilities.commonReady ? ColorCommonReady : ColorNotReady;
        specialIcon.color = playerAbilities.specialReady ? ColorSpecialReady : ColorNotReady;
        epicIcon.color = playerAbilities.epicReady ? ColorEpicReady : ColorNotReady;
    }
}