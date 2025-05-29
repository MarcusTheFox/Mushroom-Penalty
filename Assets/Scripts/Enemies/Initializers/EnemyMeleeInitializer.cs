using Enemies.CoreLogic;
using Enemies.Data;
using UnityEditor;
using UnityEngine;

namespace Enemies.Initializers
{
    public class EnemyMeleeInitializer : EnemyInitializer
    {
        [SerializeField] private EnemyMeleeDataSO enemyData;
        
        protected override void CreateEnemy()
        {
            EnemyMelee enemyMelee = new EnemyMelee(Context, enemyData);
            enemyMelee.Initialize();
            Health = enemyMelee.CoreComponents.Health;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Vector3 position = transform.position;
            Vector3 normal = Vector3.up;

            Color originalColor = Handles.color;

            Handles.color = Color.green * 0.2f;
            Handles.DrawSolidDisc(position, normal, enemyData.idleChaseRadius);

            Handles.color = Color.yellow * 0.2f;
            Handles.DrawSolidDisc(position, normal, enemyData.chaseIdleRadius);

            Handles.color = Color.red * 0.2f;
            Handles.DrawSolidDisc(position, normal, enemyData.chaseAttackRadius);
        
            Handles.color = Color.blue * 0.2f;
            Handles.DrawSolidDisc(position, normal, enemyData.attackChaseRadius);
        
            Handles.color = Color.green * 0.8f;
            Handles.DrawWireDisc(position, normal, enemyData.idleChaseRadius);

            Handles.color = Color.yellow * 0.8f;
            Handles.DrawWireDisc(position, normal, enemyData.chaseIdleRadius);

            Handles.color = Color.red * 0.8f;
            Handles.DrawWireDisc(position, normal, enemyData.chaseAttackRadius);

            Handles.color = Color.blue * 0.8f;
            Handles.DrawWireDisc(position, normal, enemyData.attackChaseRadius);
        }
#endif
    }
}
