using UnityEngine;
using UnityEngine.SceneManagement;
using Core.Save.Models;
using Core.Save.Repository;
using Player.Initializers;

namespace Core.Save
{
    public class SaveGameInteractor : Singleton<SaveGameInteractor>, IInitializable
    {
        private ISaveRepository saveRepository;
        private GameObject gameObject;

        public void Initialize()
        {
            gameObject = new GameObject("SaveGameInteractor");
            Object.DontDestroyOnLoad(gameObject);
            saveRepository = new JsonSaveRepository();
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
                    if (playerInitializer.Health.Health < saveData.Player.Health)
                        playerInitializer.Health.Increase(saveData.Player.Health - playerInitializer.Health.Health);
                    else if (playerInitializer.Health.Health > saveData.Player.Health)
                        playerInitializer.Health.Decrease(playerInitializer.Health.Health - saveData.Player.Health);
                }

                player.transform.position = saveData.Player.Position;
                player.transform.eulerAngles = saveData.Player.Rotation;
            }

            Debug.Log("Игра загружена");
        }

        public bool HasSave()
        {
            return saveRepository.HasSave();
        }
    }
} 