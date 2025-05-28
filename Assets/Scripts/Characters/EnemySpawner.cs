using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject[] enemyPrefabs; // ← теперь массив
    [SerializeField] private GameObject bossPrefab;

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private int maxSpawnCount = 10;

    [Header("Player Tracking")]
    [SerializeField] private Transform playerTransform;

    [Header("Activation Settings")]
    [SerializeField] private float spawnActivationDistance = 15f;

    [Header("Visualization")]
    [SerializeField] private Color gizmoColor = new Color(1f, 1f, 0f, 0.25f);

    private float timer = 0f;
    private int currentSpawned = 0;
    private bool playerWasInRange = false;
    private int enemiesKilled = 0;
    private const int killThreshold = 3;

    private void Awake()
    {
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                playerTransform = playerObj.transform;
            else
                Debug.LogError("Player с тегом 'Player' не найден!");
        }
    }

    private void Update()
    {
        if (playerTransform == null || currentSpawned >= maxSpawnCount)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        bool playerInRange = distanceToPlayer <= spawnActivationDistance;

        if (playerInRange && !playerWasInRange)
        {
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
        if (currentSpawned >= maxSpawnCount || enemyPrefabs.Length == 0 || spawnPoints.Length == 0)
            return;

        int enemyIndex = Random.Range(0, enemyPrefabs.Length); // случайный враг
        GameObject selectedEnemyPrefab = enemyPrefabs[enemyIndex];

        int pointIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[pointIndex];

        GameObject enemyObj = Instantiate(selectedEnemyPrefab, spawnPoint.position, spawnPoint.rotation);
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
        GameManager.Instance.AddScore(1);

        if (enemiesKilled >= killThreshold && bossPrefab != null)
        {
            GameObject bossObj = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
            Boss boss = bossObj.GetComponent<Boss>();

            if (boss != null)
            {
                boss.InitializePlayer(playerTransform);
                boss.OnBossDead += () => GameManager.Instance.AddScore(2);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, spawnActivationDistance);
    }
}
