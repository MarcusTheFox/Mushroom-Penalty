using Enemies.CoreLogic;
using UnityEngine;

namespace Enemies.Initializers
{
    public class EnemyMagicInitializer : EnemyInitializer
    {
        [SerializeField] private Transform fireballSpawnPoint;
        
        protected override void CreateEnemy()
        {
            EnemyMagic enemyMelee = new EnemyMagic(UEL, AEL, IOE, UI, transform, animator, player.transform);

            enemyMelee.Initialize(enemyData);
        }
    }
}
