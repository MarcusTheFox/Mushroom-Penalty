using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public static class SceneLoader
    {
        public static void LoadScene(int index)
        {
            ResetTimeScale();
            SceneManager.LoadScene(index);
        }
        
        public static void RestartCurrentScene()
        {
            ResetTimeScale();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public static void LoadNextScene()
        {
            ResetTimeScale();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private static void ResetTimeScale()
        {
            Time.timeScale = 1f;
        }
    }
}