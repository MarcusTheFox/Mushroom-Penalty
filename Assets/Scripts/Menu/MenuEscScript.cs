using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEscScript : MonoBehaviour
{
    public void OnMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }
}
