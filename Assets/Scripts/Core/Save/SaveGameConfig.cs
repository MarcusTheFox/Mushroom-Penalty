using UnityEngine;

namespace Core.Save
{
    [CreateAssetMenu(fileName = "SaveGameConfig", menuName = "Config/SaveGameConfig")]
    public class SaveGameConfig : ScriptableObject
    {
        [SerializeField] private GameObject enemyMeleePrefab;
        [SerializeField] private GameObject enemyMagicPrefab;
        [SerializeField] private GameObject bossPrefab;

        public GameObject EnemyMeleePrefab => enemyMeleePrefab;
        public GameObject EnemyMagicPrefab => enemyMagicPrefab;
        public GameObject BossPrefab => bossPrefab;
    }
} 