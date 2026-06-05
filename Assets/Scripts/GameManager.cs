using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Tiempo")]
    public float matchDuration = 600f; // 10 minutos
    private float timeRemaining;
    private bool gameOver = false;

    [Header("Jugadores")]
    public GameObject player1;
    public GameObject player2;

    void Awake()
    {
        Instance = this;
        timeRemaining = matchDuration;
    }

    void Update()
    {
        if (gameOver) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            EndGame("Empate");
        }
    }

    public void PlayerEliminated(GameObject player)
    {
        if (gameOver) return;

        if (player == player1)
            EndGame("¡Jugador 2 gana!");
        else
            EndGame("¡Jugador 1 gana!");
    }

    void EndGame(string result)
    {
        gameOver = true;
        Time.timeScale = 0f; // pausa el juego
        UIManager.Instance.ShowResult(result);
    }

    public float GetTimeRemaining() => timeRemaining;
    public bool IsGameOver() => gameOver;

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}