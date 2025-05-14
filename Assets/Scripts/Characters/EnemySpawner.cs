using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject enemyPrefab;
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
    }

    private void Update()
    {
        if (playerTransform == null || currentSpawned >= maxSpawnCount)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        bool playerInRange = distanceToPlayer <= spawnActivationDistance;

        if (playerInRange && !playerWasInRange)
        {
            // »грок только что вошЄл в зону Ч спавним сразу
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
        }

        currentSpawned++;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, spawnActivationDistance);
    }
}
