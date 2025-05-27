using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Text scoreText;

    private int score = 0;
    private const int VICTORY_SCORE = 5;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();

        if (score >= VICTORY_SCORE)
        {
            Invoke(nameof(LoadVictoryScene), 3f);
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"Scores: {score}";
    }

    private void LoadVictoryScene()
    {
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
        if (player != null)
        {
            Destroy(player.gameObject);
        }

        SceneManager.LoadScene("WinScene");
    }
}
