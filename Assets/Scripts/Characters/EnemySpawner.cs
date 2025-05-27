using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private GameObject bossPrefab;

    private int enemiesKilled = 0;
    private const int killThreshold = 3;

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private int maxSpawnCount = 10;

    [Header("Player Tracking")]
    [SerializeField] private Transform playerTransform;

    [Header("Activation Settings")]
    [SerializeField] private float spawnActivationDistance = 15f;

    [Header("Visualization")]
    [SerializeField] private Color gizmoColor = new Color(1f, 1f, 0f, 0.25f);

    [Header("UI Settings")]
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject victoryScreen;

    private float timer = 0f;
    private int currentSpawned = 0;
    private bool playerWasInRange = false;

    private int score = 0;


    private void Awake()
    {
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                playerTransform = playerObj.transform;
            else
                Debug.LogError("Player with tag 'Player' not found!");
        }

        UpdateScoreUI();

        if (victoryScreen != null)
            victoryScreen.SetActive(false);
    }

    private void Update()
    {
        if (playerTransform == null || currentSpawned >= maxSpawnCount)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        bool playerInRange = distanceToPlayer <= spawnActivationDistance;

        if (playerInRange && !playerWasInRange)
        {
            // Игрок только что вошёл в зону — спавним сразу
            SpawnEnemyAtRandomPoint();
            timer = 0f;
        }

        if (playerInRange)
        {
            timer += Time.deltaTime;

            if (timer >= spawnInterval)
            {
                SpawnEnemyAtRandomPoint();
                timer = 0f;
            }
        }

        playerWasInRange = playerInRange;
    }

    private void SpawnEnemyAtRandomPoint()
    {
        if (currentSpawned >= maxSpawnCount || enemyPrefab == null || spawnPoints.Length == 0)
            return;

        int index = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];

        GameObject enemyObj = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        Enemy enemy = enemyObj.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Initialize(playerTransform);
            enemy.OnDead += () =>
            {
                OnEnemyKilled(spawnPoint.position);
            };
        }

        currentSpawned++;
    }

    private void OnEnemyKilled(Vector3 spawnPosition)
    {
        enemiesKilled++;
        AddScore(1);

        if (enemiesKilled >= killThreshold && bossPrefab != null)
        {
            GameObject bossObj = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);

            Boss boss = bossObj.GetComponent<Boss>();
            if (boss != null)
            {
                GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
                if (playerObj != null)
                {
                    boss.InitializePlayer(playerObj.transform);
                    boss.OnBossDead += () => OnBossKilled();
                }
                else
                {
                    Debug.LogError("Player с тегом 'Player' не найден!");
                }
            }

        }
    }

    private void OnBossKilled()
    {
        AddScore(2); // ← очки за босса
    }

    private void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();

        if (score >= 5)
        {
            ShowVictoryScreen();
            // Можно дополнительно остановить игру, отключить спавн и управление игроком
            enabled = false;
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Scores: {score}";
        }
    }

    private void ShowVictoryScreen()
    {
        if (victoryScreen != null)
            victoryScreen.SetActive(true);

        Debug.Log("Победа! Игрок набрал 10 очков.");
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, spawnActivationDistance);
    }
}
