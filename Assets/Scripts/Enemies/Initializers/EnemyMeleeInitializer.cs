using Enemies.CoreLogic;
using UnityEditor;
using UnityEngine;

namespace Enemies.Initializers
{
    public class EnemyMeleeInitializer : EnemyInitializer
    {
        protected override void CreateEnemy()
        {
            EnemyMelee enemyMelee = new EnemyMelee(UEL, AEL, IOE, UI, transform, animator, player.transform);
            
            enemyMelee.Initialize(enemyData);
        }

        private void OnDrawGizmos()
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
    }
}
