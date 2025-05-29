using UnityEngine;

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
    }

    private void LoadMainMenu()
    {
        SceneController.Instance.LoadMainMenu();
    }
} 