using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    private void Start()
    {
        LoadInitialLevel();
    }

    private void LoadInitialLevel()
    {
        SceneManager.LoadScene("GameProcessScene");
    }
}
