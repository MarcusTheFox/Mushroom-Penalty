using Enemies.CoreLogic;
using Enemies.Data;
using UnityEngine;

namespace Enemies.Initializers
{
    public class BossInitializer : EnemyInitializer
    {
        [SerializeField] private BossDataSO enemyData;
        
        protected override void CreateEnemy()
        {
            Boss boss = new Boss(Context, enemyData);
            boss.Initialize();
            Health = boss.CoreComponents.Health;
        }
    }
}
