using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("Main Menu Buttons")]
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button settingsButton;
    
        [Header("Settings Panel")]
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private Button settingsBackButton;
    
        private void Start()
        {
            InitializeButtons();
            InitializeSettings();
        }

        private void InitializeButtons()
        {
            if (newGameButton != null)
                newGameButton.onClick.AddListener(OnNewGameClick);
            
            if (continueButton != null)
                continueButton.onClick.AddListener(OnContinueClick);
            
            if (settingsButton != null)
                settingsButton.onClick.AddListener(OnSettingsClick);

            if (settingsBackButton != null)
                settingsBackButton.onClick.AddListener(OnSettingsBackClick);
        }

        private void InitializeSettings()
        {
            if (settingsPanel != null)
                settingsPanel.SetActive(false);
            
            if (volumeSlider != null)
            {
                volumeSlider.value = AudioManager.Instance.GetVolume();
                volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
            }
        }

        private void OnNewGameClick()
        {
            SceneController.Instance.LoadGame();
        }

        private void OnContinueClick()
        {
            SceneController.Instance.LoadGame();
        }

        private void OnSettingsClick()
        {
            if (settingsPanel != null)
                settingsPanel.SetActive(!settingsPanel.activeSelf);
        }

        private void OnVolumeChanged(float value)
        {
            AudioManager.Instance.SetVolume(value);
        }

        private void OnSettingsBackClick()
        {
            if (settingsPanel != null)
                settingsPanel.SetActive(false);
        }
    }
} 