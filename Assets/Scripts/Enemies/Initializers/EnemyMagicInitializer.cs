using Enemies.CoreLogic;
using Enemies.Data;
using UnityEditor;
using UnityEngine;

namespace Enemies.Initializers
{
    public class EnemyMagicInitializer : EnemyInitializer
    {
        [SerializeField] private Transform fireballSpawnPoint;
        [SerializeField] private EnemyMagicDataSO enemyData;
        
        protected override void CreateEnemy()
        {
            EnemyMagic enemyMagic = new EnemyMagic(Context, fireballSpawnPoint, enemyData);
            
            enemyMagic.Initialize();
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Vector3 position = transform.position;
            Vector3 normal = Vector3.up;

            Color originalColor = Handles.color;
            Color orange = new Color(1, 0.3f, 0);

            Handles.color = Color.green * 0.2f;
            Handles.DrawSolidDisc(position, normal, enemyData.idleChaseRadius);

            Handles.color = Color.yellow * 0.2f;
            Handles.DrawSolidDisc(position, normal, enemyData.chaseIdleRadius);

            Handles.color = Color.red * 0.2f;
            Handles.DrawSolidDisc(position, normal, enemyData.chaseAttackRadius);
        
            Handles.color = orange * 0.2f;
            Handles.DrawSolidDisc(position, normal, enemyData.attackChaseRadius);
            
            Handles.color = Color.blue * 0.2f;
            Handles.DrawSolidDisc(position, normal, enemyData.attackFleeRadius);
            
            Handles.color = Color.cyan * 0.2f;
            Handles.DrawSolidDisc(position, normal, enemyData.fleeAttackRadius);
        
            Handles.color = Color.green * 0.8f;
            Handles.DrawWireDisc(position, normal, enemyData.idleChaseRadius);

            Handles.color = Color.yellow * 0.8f;
            Handles.DrawWireDisc(position, normal, enemyData.chaseIdleRadius);

            Handles.color = Color.red * 0.8f;
            Handles.DrawWireDisc(position, normal, enemyData.chaseAttackRadius);

            Handles.color = orange * 0.8f;
            Handles.DrawWireDisc(position, normal, enemyData.attackChaseRadius);
            
            Handles.color = Color.blue * 0.8f;
            Handles.DrawWireDisc(position, normal, enemyData.attackFleeRadius);
            
            Handles.color = Color.cyan * 0.8f;
            Handles.DrawWireDisc(position, normal, enemyData.fleeAttackRadius);
        }
#endif
    }
}
