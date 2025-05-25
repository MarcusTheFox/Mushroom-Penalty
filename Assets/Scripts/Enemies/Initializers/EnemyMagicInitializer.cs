using Enemies.CoreLogic;

namespace Enemies.Initializers
{
    public class EnemyMagicInitializer : EnemyInitializer
    {
        protected override void CreateEnemy()
        {
            EnemyMagic enemyMelee = new EnemyMagic(UEL, AEL, IOE, UI, transform, animator);

            enemyMelee.Initialize();
        }
    }
}
