using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Timer")]
    public TextMeshProUGUI timerText;

    [Header("Resultado")]
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    public UnityEngine.UI.Button restartButton;
    public UnityEngine.UI.Button menuButton;

    [Header("Barras de vida")]
    public UnityEngine.UI.Image healthBarP1;
    public UnityEngine.UI.Image healthBarP2;

    public PlayerHealth p1Health;
    public PlayerHealth p2Health;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        resultPanel.SetActive(false);

        restartButton.onClick.AddListener(() => GameManager.Instance.RestartGame());
        menuButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        });
    }

    void Update()
    {
        if (GameManager.Instance == null) return;
        float t = GameManager.Instance.GetTimeRemaining();
        int minutes = Mathf.FloorToInt(t / 60f);
        int seconds = Mathf.FloorToInt(t % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";

        if (p1Health != null && healthBarP1 != null)
            healthBarP1.fillAmount = p1Health.currentHP / p1Health.maxHP;

        if (p2Health != null && healthBarP2 != null)
            healthBarP2.fillAmount = p2Health.currentHP / p2Health.maxHP;
    }

    public void ShowResult(string result)
    {
        resultPanel.SetActive(true);
        resultText.text = result;
    }
}
