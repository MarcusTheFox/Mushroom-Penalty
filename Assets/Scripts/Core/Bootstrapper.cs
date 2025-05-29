using UnityEngine;
using Core.Save;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        private void Start()
        {
            InitializeManagers();
            LoadMainMenu();
        }

        private void InitializeManagers()
        {
            var audioManager = AudioManager.Instance;
            audioManager.Initialize();

            var sceneController = SceneController.Instance;

            var saveManager = SaveGameInteractor.Instance;
            saveManager.Initialize();
        }

        private void LoadMainMenu()
        {
            SceneController.Instance.LoadMainMenu();
        }
    }
}