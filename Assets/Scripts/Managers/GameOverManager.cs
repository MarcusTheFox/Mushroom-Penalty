using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameOverManager : MonoBehaviour
{
    public void OnNewGame()
    {
        SceneManager.LoadScene("GameProcessScene");
    }
}
