using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Round Message")]
    public TMP_Text messageText;

    [Header("Score Display")]
    public TMP_Text player1ScoreText;
    public TMP_Text player2ScoreText;

    [Header("Win Screen")]
    public GameObject winScreenPanel;
    public TMP_Text winnerText;
    public TMP_Text finalScoreText;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // --- Round message ---

    public void ShowMessage(string message, float duration)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);
        Invoke(nameof(HideMessage), duration);
    }

    void HideMessage()
    {
        messageText.gameObject.SetActive(false);
    }

    // --- Score display ---

    public void UpdateScores(int p1, int p2)
    {
        if (player1ScoreText != null) player1ScoreText.text = $"P1: {p1}";
        if (player2ScoreText != null) player2ScoreText.text = $"P2: {p2}";
    }

    // --- Win screen ---

    public void ShowWinScreen(string winner, int p1Score, int p2Score)
    {
        if (winScreenPanel == null) return;

        winnerText.text = $"{winner} Wins!";
        finalScoreText.text = $"P1  {p1Score}  —  {p2Score}  P2";
        winScreenPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // Called by "Play Again" button on the win screen
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Called by "Main Menu" button on the win screen
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
