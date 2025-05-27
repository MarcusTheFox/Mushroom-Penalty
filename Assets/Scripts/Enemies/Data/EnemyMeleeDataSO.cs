using UnityEngine;

namespace Enemies.Data
{
    [CreateAssetMenu(fileName = "EnemyMeleeDataSO", menuName = "Scriptable Objects/EnemyMeleeDataSO")]
    public class EnemyMeleeDataSO : EnemyDataSO
    {
        [Header("Movement")]
        public float speed = 5f;
        [Header("Attack")]
        public float meleeDamage = 10f;
        public float meleeRange = 7f;
        public float meleeAngle = 90f;
        [Header("State radius")] 
        public float idleChaseRadius = 10f;
        public float chaseIdleRadius = 15f;
        public float chaseAttackRadius = 2f;
        public float attackChaseRadius = 7f;
    }
}
