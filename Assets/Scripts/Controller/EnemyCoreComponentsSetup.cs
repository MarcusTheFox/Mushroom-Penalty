using Model;

namespace Controller
{
    public class EnemyCoreComponentsSetup
    {
        public IHealth Health { get; private set; }
        public IDamageable Damageable { get; private set; }

        public EnemyCoreComponentsSetup(float health)
        {
            Health = new HealthComponent(health);
        }
        
        public void Initialize()
        {
            Damageable = new DamageableComponent(Health);
        }
    }
}