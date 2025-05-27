using UnityEngine;

namespace Enemies.Data
{
    [CreateAssetMenu(fileName = "EnemyDataSO", menuName = "Scriptable Objects/EnemyDataSO")]
    public abstract class EnemyDataSO : ScriptableObject
    {
        public float health = 100f;
        [Space]
        public LayerMask targetLayer;
    }
}
