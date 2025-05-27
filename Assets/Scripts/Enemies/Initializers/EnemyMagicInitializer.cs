using Enemies.CoreLogic;
using Enemies.Data;
using UnityEngine;

namespace Enemies.Initializers
{
    public class EnemyMagicInitializer : EnemyInitializer
    {
        [SerializeField] private Transform fireballSpawnPoint;
        [SerializeField] private EnemyMagicDataSO enemyData;
        
        protected override void CreateEnemy()
        {
            EnemyMagic enemyMagic = new EnemyMagic(UEL,
                AEL,
                IOE,
                UI,
                transform,
                animator,
                player.transform,
                fireballSpawnPoint);
            
            enemyMagic.Configure(enemyData);
            enemyMagic.Initialize();
        }
    }
}
