using UnityEngine.SceneManagement;

namespace Core
{
    public class SceneController : Singleton<SceneController>
    {
        public const string BOOTSTRAP_SCENE = "BootstrapScene";
        public const string MENU_SCENE = "Menu";
        public const string GAME_SCENE = "GameProcessScene";

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(MENU_SCENE);
        }

        public void LoadGame()
        {
            SceneManager.LoadScene(GAME_SCENE);
        }

        public void LoadBootstrap()
        {
            SceneManager.LoadScene(BOOTSTRAP_SCENE);
        }

        public void RestartCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}