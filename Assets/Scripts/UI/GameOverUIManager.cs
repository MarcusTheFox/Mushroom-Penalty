using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameOverUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverUI;
        [SerializeField] private Button restartButton;

        private void Awake()
        {
            gameOverUI.SetActive(false);
            restartButton.onClick.AddListener(HandleRestartClicked);
        }

        private void OnDestroy()
        {
            restartButton.onClick.RemoveListener(HandleRestartClicked);
        }

        public void ShowGameOverPanel()
        {
            gameOverUI.SetActive(true);
            Time.timeScale = 0f;
        }

        private void HandleRestartClicked()
        {
            SceneController.Instance.RestartCurrentScene();
        }
    }
}