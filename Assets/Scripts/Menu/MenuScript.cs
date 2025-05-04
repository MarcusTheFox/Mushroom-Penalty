using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Settings;

    public void OnNewGame()
    {
        SceneManager.LoadScene("GameProcessScene");
    }

    public void OnSettingsEnter()
    {
       Settings.SetActive(true);
       Menu.SetActive(false);

    }

    public void OnSettingsExit()
    {
        Menu.SetActive(true);
        Settings.SetActive(false);
    }
}
