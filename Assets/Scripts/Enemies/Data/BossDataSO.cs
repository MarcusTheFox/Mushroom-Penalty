using UnityEngine;

namespace Enemies.Data
{
    [CreateAssetMenu(fileName = "BossDataSO", menuName = "Scriptable Objects/BossDataSO")]
    public class BossDataSO : EnemyDataSO
    {
        [Header("Movement")]
        public float speed;
        [Header("Attack")]
        public float meleeDamage;
        public float meleeRange;
        public float meleeAngle;
        [Header("State radius")]
        public float idleChaseRadius;
        public float chaseAttackRadius;
        public float attackChaseRadius;
        [Header("Strong attack")]
        public float strongMeleeDamage;
        public float strongMeleeRange;
        public float strongMeleeAngle;
        public int attacksNumberForStrongAttack = 3;
        [Header("Block & heal")]
        public float blockHealDuration;
        public float healPerSecond;
        [Range(0.01f, 0.99f)]
        public float healthThresholdForHealing = 0.33f;
        [Header("Explosion")]
        public float explosionDamage;
        public float explosionRadius;
    }
}
