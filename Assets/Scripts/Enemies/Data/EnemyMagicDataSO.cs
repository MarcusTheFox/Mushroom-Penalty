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
        [Min(0)]
        public float idleChaseRadius;
        [Min(0)]
        public float chaseIdleRadius;
        [Min(0)]
        public float chaseAttackRadius;
        [Min(0)]
        public float attackChaseRadius;
        [Min(0)]
        public float attackFleeRadius;
        [Min(0)]
        public float fleeAttackRadius;
    }
}
