using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject soundManagerPrefab;
    
    private void Start()
    {
        InitializeSoundManager();
        
        LoadInitialLevel();
    }

    private void InitializeSoundManager()
    {
        GameObject soundManagerObject = Instantiate(soundManagerPrefab);
        soundManagerObject.name = "SoundManager";
    }

    private void LoadInitialLevel()
    {
        SceneManager.LoadScene("Menu");
    }


}
