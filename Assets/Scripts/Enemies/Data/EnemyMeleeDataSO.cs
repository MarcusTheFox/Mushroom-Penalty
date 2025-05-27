using UnityEngine;

namespace Enemies.Data
{
    [CreateAssetMenu(fileName = "EnemyMeleeDataSO", menuName = "Scriptable Objects/EnemyMeleeDataSO")]
    public class EnemyMeleeDataSO : EnemyDataSO
    {
        [Header("Movement")]
        public float speed = 5f;
        [Header("Attack")]
        public float meleeDamage = 20f;
        public float meleeRange = 10f;
        public float meleeAngle = 90f;
        [Header("State radius")] 
        public float idleChaseRadius = 25f;
        public float chaseIdleRadius = 35f;
        public float chaseAttackRadius = 7f;
        public float attackChaseRadius = 10f;
    }
}
