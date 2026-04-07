using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Association: GameManager references Players but doesn't own them
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Players")]
    public GameObject player1;
    public GameObject player2;

    [Header("Spawn Points")]
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    [Header("Score")]
    public int player1Score = 0;
    public int player2Score = 0;
    public int roundsToWin = 3;

    private bool roundOver = false;

    public GameObject PauseMenu;
    private static bool isPaused = false;
    private static bool gameOver = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // Listen to death events
        player1.GetComponent<PlayerHealth>().onDeath.AddListener(OnPlayer1Died);
        player2.GetComponent<PlayerHealth>().onDeath.AddListener(OnPlayer2Died);
        
    }

    void Update()
    {
        if ((Keyboard.current.escapeKey.wasPressedThisFrame || Keyboard.current.pKey.wasPressedThisFrame) && !gameOver)
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }
    void OnPlayer1Died()
    {
        if (roundOver) return;
        roundOver = true;
        player2Score++;
        if (UIManager.instance != null) UIManager.instance.UpdateScores(player1Score, player2Score);
        if (UIManager.instance != null) UIManager.instance.ShowMessage("Player 2 wins the round!", 1.5f);
        Invoke(nameof(CheckWinOrRestart), 2f);
    }

    void OnPlayer2Died()
    {
        if (roundOver) return;
        roundOver = true;
        player1Score++;
        if (UIManager.instance != null) UIManager.instance.UpdateScores(player1Score, player2Score);
        if (UIManager.instance != null) UIManager.instance.ShowMessage("Player 1 wins the round!", 1.5f);
        Invoke(nameof(CheckWinOrRestart), 2f);
    }

    void CheckWinOrRestart()
    {
        if (player1Score >= roundsToWin)
        {
            gameOver = true;
            if (UIManager.instance != null) UIManager.instance.ShowWinScreen("Player 1", player1Score, player2Score);
        }
        else if (player2Score >= roundsToWin)
        {
            gameOver = true;
            if (UIManager.instance != null) UIManager.instance.ShowWinScreen("Player 2", player1Score, player2Score);
        }
        else
        {
            RestartRound();
        }
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameOver = false;
    }
    void RestartRound()
    {
        roundOver = false;

        // Reset positions
        player1.transform.position = spawnPoint1.position;
        player2.transform.position = spawnPoint2.position;

        // Reset health
        player1.SetActive(true);
        player1.GetComponent<PlayerHealth>().ResetHealth();
        player2.SetActive(true);
        player2.GetComponent<PlayerHealth>().ResetHealth();

        //reset guns
        Gun.DestroyAllGuns();
    }

    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }   

    public static bool getIsPaused()
    {
        return isPaused;
    }

    public static bool getGameOver()
    {
        return gameOver;
    }
}