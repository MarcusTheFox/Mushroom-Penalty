using Enemies.CoreLogic;
using Enemies.Data;
using UnityEngine;

namespace Enemies.Initializers
{
    public class BossInitializer : EnemyInitializer
    {
        [SerializeField] private EnemyMeleeDataSO enemyData;
        
        protected override void CreateEnemy()
        {
            Boss boss = new Boss(UEL,
                AEL,
                IOE,
                UI,
                transform,
                animator,
                player.transform);
            
            boss.Configure(enemyData);
            boss.Initialize();
        }
    }
}
