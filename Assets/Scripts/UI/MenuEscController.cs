using UnityEngine;
using UnityEngine.UI;
using Player.Input;
using Core;
using Core.Save;

namespace UI
{
    public class MenuEscController : MonoBehaviour
    {
        [Header("Menu")]
        [SerializeField] private GameObject pauseMenuPanel;
        [SerializeField] private PlayerInputController playerInput;

        [Header("Buttons")]
        [SerializeField] private Button continueButton;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button saveButton;
        [SerializeField] private Button loadButton;
        
        private bool isPaused;

        private void Start()
        {
            if (pauseMenuPanel != null)
                pauseMenuPanel.SetActive(false);
                
            if (playerInput != null)
                playerInput.Menu += OnMenuToggle;

            SetupButtons();
            UpdateLoadButtonState();
        }

        private void OnDestroy()
        {
            if (playerInput != null)
                playerInput.Menu -= OnMenuToggle;
        }

        private void SetupButtons()
        {
            if (continueButton != null)
                continueButton.onClick.AddListener(ContinueGame);

            if (mainMenuButton != null)
                mainMenuButton.onClick.AddListener(GoToMainMenu);

            if (saveButton != null)
                saveButton.onClick.AddListener(SaveGame);

            if (loadButton != null)
                loadButton.onClick.AddListener(LoadGame);
        }

        private void UpdateLoadButtonState()
        {
            if (loadButton != null)
                loadButton.interactable = SaveGameInteractor.Instance.HasSave();
        }

        private void OnMenuToggle()
        {
            if (isPaused)
                ContinueGame();
            else
                PauseGame();
        }

        private void PauseGame()
        {
            isPaused = true;
            Time.timeScale = 0f;
            
            if (pauseMenuPanel != null)
                pauseMenuPanel.SetActive(true);

            playerInput.SetMovementInputEnabled(false);
            playerInput.SetMeleeAttackInputEnabled(false);
            playerInput.SetMagicAttackInputEnabled(false);

            UpdateLoadButtonState();
        }

        private void ContinueGame()
        {
            isPaused = false;
            Time.timeScale = 1f;
            
            if (pauseMenuPanel != null)
                pauseMenuPanel.SetActive(false);

            playerInput.SetMovementInputEnabled(true);
            playerInput.SetMeleeAttackInputEnabled(true);
            playerInput.SetMagicAttackInputEnabled(true);
        }

        private void GoToMainMenu()
        {
            Time.timeScale = 1f;
            SceneController.Instance.LoadMainMenu();
        }

        private void SaveGame()
        {
            SaveGameInteractor.Instance.SaveGame();
            UpdateLoadButtonState();
        }

        private void LoadGame()
        {
            ContinueGame();
            SaveGameInteractor.Instance.LoadGame();
        }

        private void OnDisable()
        {
            if (continueButton != null)
                continueButton.onClick.RemoveListener(ContinueGame);

            if (mainMenuButton != null)
                mainMenuButton.onClick.RemoveListener(GoToMainMenu);

            if (saveButton != null)
                saveButton.onClick.RemoveListener(SaveGame);

            if (loadButton != null)
                loadButton.onClick.RemoveListener(LoadGame);
        }
    }
} 