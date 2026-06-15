using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterSelection : MonoBehaviour
{
    [Header("Botones Jugador 1")]
    public Button p1Arashiko;
    public Button p1MariaElena;
    public Button p1Jeffrey;

    [Header("Botones Jugador 2")]
    public Button p2Arashiko;
    public Button p2MariaElena;
    public Button p2Jeffrey;

    [Header("Selección actual")]
    public TextMeshProUGUI p1SelectionText;
    public TextMeshProUGUI p2SelectionText;

    public Button startButton;

    private CharacterType p1Choice = CharacterType.Arashiko;
    private CharacterType p2Choice = CharacterType.Arashiko;
    private bool p1Selected = false;
    private bool p2Selected = false;

    void Start()
    {
        p1Arashiko.onClick.AddListener(() => SelectP1(CharacterType.Arashiko, "Arashiko"));
        p1MariaElena.onClick.AddListener(() => SelectP1(CharacterType.MariaElena, "MariaElena"));
        p1Jeffrey.onClick.AddListener(() => SelectP1(CharacterType.Jeffrey, "Jeffrey"));

        p2Arashiko.onClick.AddListener(() => SelectP2(CharacterType.Arashiko, "Arashiko"));
        p2MariaElena.onClick.AddListener(() => SelectP2(CharacterType.MariaElena, "MariaElena"));
        p2Jeffrey.onClick.AddListener(() => SelectP2(CharacterType.Jeffrey, "Jeffrey"));

        startButton.onClick.AddListener(StartGame);
        startButton.interactable = false;
    }

    void SelectP1(CharacterType type, string name)
    {
        p1Choice = type;
        p1Selected = true;
        p1SelectionText.text = $"Elegido: {name}";
        UpdateStartButton();
    }

    void SelectP2(CharacterType type, string name)
    {
        p2Choice = type;
        p2Selected = true;
        p2SelectionText.text = $"Elegido: {name}";
        UpdateStartButton();
    }

    void UpdateStartButton()
    {
        startButton.interactable = p1Selected && p2Selected;
    }

    void StartGame()
    {
        GameData.p1Character = p1Choice;
        GameData.p2Character = p2Choice;
        SceneManager.LoadScene("GameScene");
    }
}