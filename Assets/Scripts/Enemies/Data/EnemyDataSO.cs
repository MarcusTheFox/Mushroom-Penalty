using UnityEngine;

namespace Enemies.Data
{
    [CreateAssetMenu(fileName = "EnemyDataSO", menuName = "Scriptable Objects/EnemyDataSO")]
    public class EnemyDataSO : ScriptableObject
    {
        public float health = 100f;
        public float chaseSpeed = 5f;
        public float fleeSpeed = 7f;
        [Space]
        [Header("State radius")]
        public float idleChaseRadius = 10f;
        public float chaseIdleRadius = 15f;
        public float chaseAttackRadius = 2f;
        public float attackChaseRadius = 7f;
    }
}
