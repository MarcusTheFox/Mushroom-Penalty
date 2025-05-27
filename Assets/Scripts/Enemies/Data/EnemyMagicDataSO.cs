using UnityEngine;

namespace Enemies.Data
{
    [CreateAssetMenu(fileName = "EnemyMagicDataSO", menuName = "Scriptable Objects/EnemyMagicDataSO")]
    public class EnemyMagicDataSO : EnemyDataSO
    {
        [Header("Movement")]
        public float speedTowards = 5f;
        public float speedAway = 7f;
        [Header("Attack")]
        public float magicDamage = 30f;
        public float magicCooldown = 1f;
        public GameObject magicProjectilePrefab;
        [Header("State radius")] 
        public float idleChaseRadius = 25f;
        public float chaseIdleRadius = 35f;
        public float chaseAttackRadius = 7f;
        public float attackChaseRadius = 10f;
    }
}
