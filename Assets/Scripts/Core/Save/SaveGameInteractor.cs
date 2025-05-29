using UnityEngine;
using UnityEngine.SceneManagement;
using Core.Save.Models;
using Core.Save.Repository;
using Player.Initializers;
using Enemies.Initializers;
using Combat.Interfaces;
namespace Core.Save
{
    public class SaveGameInteractor : Singleton<SaveGameInteractor>, IInitializable
    {
        private ISaveRepository saveRepository;
        private GameObject gameObject;
        private SaveGameConfig config;

        public void Initialize()
        {
            gameObject = new GameObject("SaveGameInteractor");
            Object.DontDestroyOnLoad(gameObject);
            saveRepository = new JsonSaveRepository();

            config = Resources.Load<SaveGameConfig>("SaveGameConfig");
            if (config == null)
            {
                Debug.LogError("SaveGameConfig not found in Resources folder!");
            }
        }

        public void SaveGame()
        {
            var saveData = new SaveData();

            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                var playerInitializer = player.GetComponent<PlayerObjectInitializer>();
                if (playerInitializer != null)
                    saveData.Player.Health = playerInitializer.Health.Health;

                saveData.Player.Position = player.transform.position;
                saveData.Player.Rotation = player.transform.eulerAngles;
            }

            saveData.GameState.GameTime = Time.time;
            saveData.GameState.CurrentScene = SceneManager.GetActiveScene().name;

            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                string enemyType = "Melee";
                if (enemy.GetComponent<EnemyMagicInitializer>() != null)
                    enemyType = "Magic";
                else if (enemy.GetComponent<BossInitializer>() != null)
                    enemyType = "Boss";

                var enemyInitializer = enemy.GetComponent<EnemyInitializer>();
                if (enemyInitializer != null)
                {
                    var enemyData = new EnemySaveData
                    {
                        Type = enemyType,
                        Health = enemyInitializer.Health.Health,
                        Position = enemy.transform.position,
                        Rotation = enemy.transform.eulerAngles
                    };
                    saveData.Enemies.Add(enemyData);
                }
            }

            saveRepository.Save(saveData);
            Debug.Log("Игра сохранена");
        }

        public void LoadGame()
        {
            if (!saveRepository.HasSave())
            {
                Debug.Log("Нет сохранённой игры");
                return;
            }

            var saveData = saveRepository.Load();

            if (SceneManager.GetActiveScene().name != saveData.GameState.CurrentScene)
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
                SceneManager.LoadScene(saveData.GameState.CurrentScene);
            }
            else
            {
                ApplyLoadedData(saveData);
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            var saveData = saveRepository.Load();
            ApplyLoadedData(saveData);
        }

        private void ApplyLoadedData(SaveData saveData)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                var playerInitializer = player.GetComponent<PlayerObjectInitializer>();
                if (playerInitializer != null && playerInitializer.Health != null)
                {
                    UpdateHealthValue(playerInitializer.Health, saveData.Player.Health);
                }

                player.transform.position = saveData.Player.Position;
                player.transform.eulerAngles = saveData.Player.Rotation;
            }

            var existingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in existingEnemies)
            {
                enemy.SetActive(false);
                Object.Destroy(enemy);
            }

            foreach (var enemyData in saveData.Enemies)
            {
                GameObject enemyPrefab = null;
                switch (enemyData.Type)
                {
                    case "Melee":
                        enemyPrefab = config.EnemyMeleePrefab;
                        break;
                    case "Magic":
                        enemyPrefab = config.EnemyMagicPrefab;
                        break;
                    case "Boss":
                        enemyPrefab = config.BossPrefab;
                        break;
                }

                if (enemyPrefab != null)
                {
                    var enemy = Object.Instantiate(enemyPrefab, enemyData.Position, Quaternion.Euler(enemyData.Rotation));
                    enemy.tag = "Enemy";

                    var enemyInitializer = enemy.GetComponent<EnemyInitializer>();
                    if (enemyInitializer != null && enemyInitializer.Health != null)
                    {
                        UpdateHealthValue(enemyInitializer.Health, enemyData.Health);
                    }
                }
            }

            Debug.Log("Игра загружена");
        }

        public bool HasSave()
        {
            return saveRepository.HasSave();
        }
        
        private void UpdateHealthValue(IHealth health, float value)
        {
            if (health.Health < value)
                health.Increase(value - health.Health);
            else if (health.Health > value)
                health.Decrease(health.Health - value);
        }
    }
} 