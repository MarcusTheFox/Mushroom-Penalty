using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject victoryScreen;

    private int score = 0;
    private int scoreToWin = 5;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        if (victoryScreen != null)
            victoryScreen.SetActive(false);

        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();

        if (score >= scoreToWin)
        {
            ShowVictoryScreen();
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"Scores: {score}";
    }

    private void ShowVictoryScreen()
    {
        if (victoryScreen != null)
            victoryScreen.SetActive(true);

        Debug.Log("Победа! Игрок набрал достаточно очков.");
    }
}
