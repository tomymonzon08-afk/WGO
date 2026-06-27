using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
        Time.timeScale = 1f;
        timeRemaining = matchDuration;

        PlayerAbilities p1Abilities = player1.GetComponent<PlayerAbilities>();
        PlayerAbilities p2Abilities = player2.GetComponent<PlayerAbilities>();
        if (p1Abilities != null) p1Abilities.characterType = GameData.p1Character;
        if (p2Abilities != null) p2Abilities.characterType = GameData.p2Character;

        player1.GetComponentInChildren<PlayerNameDisplay>()?.SetName(GameData.p1Character);
        player2.GetComponentInChildren<PlayerNameDisplay>()?.SetName(GameData.p2Character);
    }

    void Update()
    {
        if (gameOver) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            CheckTimeUp();
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
        Time.timeScale = 0f;
        UIManager.Instance.ShowResult(result);
    }

    void CheckTimeUp()
    {
        PlayerHealth hp1 = player1.GetComponent<PlayerHealth>();
        PlayerHealth hp2 = player2.GetComponent<PlayerHealth>();

        if (hp1 == null || hp2 == null) return;

        if (hp1.currentHP > hp2.currentHP)
            EndGame("¡Jugador 1 gana!");
        else if (hp2.currentHP > hp1.currentHP)
            EndGame("¡Jugador 2 gana!");
        else
            EndGame("¡Empate!");
    }

    public float GetTimeRemaining() => timeRemaining;
    public bool IsGameOver() => gameOver;

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private bool countdownStarted = false;

    
}