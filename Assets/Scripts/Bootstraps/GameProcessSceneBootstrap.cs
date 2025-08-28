using UnityEngine;

public class GameProcessSceneBootstrap : MonoBehaviour
{
    void Start()
    {
        PlayLevelMusic();
    }
    
    void PlayLevelMusic()
    {
        SoundManager.Instance.PlayMusic("Birds");
    }
}