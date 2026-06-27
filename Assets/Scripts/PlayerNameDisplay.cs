using UnityEngine;
using TMPro;

public class PlayerNameDisplay : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        // Hace que el texto siempre mire hacia la cámara
        transform.LookAt(transform.position + cameraTransform.forward);
    }

    public void SetName(CharacterType type)
    {
        nameText.text = type switch
        {
            CharacterType.Arashiko => "Arashiko",
            CharacterType.MariaElena => "Psicogirl",
            CharacterType.Jeffrey => "Jeffrey",
            _ => ""
        };
    }
}