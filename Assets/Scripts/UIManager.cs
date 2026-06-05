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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
    }

    void Update()
    {
        if (GameManager.Instance == null) return;
        float t = GameManager.Instance.GetTimeRemaining();
        int minutes = Mathf.FloorToInt(t / 60f);
        int seconds = Mathf.FloorToInt(t % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void ShowResult(string result)
    {
        resultPanel.SetActive(true);
        resultText.text = result;
    }
}
