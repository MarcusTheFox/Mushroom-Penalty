using Combat.Implementations;
using Combat.Interfaces;
using Core.Interfaces;
using Core.UnityHooks;

namespace Enemies.Components
{
    public class EnemyCoreComponentsSetup : ICleanupable
    {
        private readonly InteractableObjectEvents IOE;
        public IHealth Health { get; private set; }
        public IDamageable Damageable { get; private set; }

        public EnemyCoreComponentsSetup(InteractableObjectEvents IOE, float health)
        {
            this.IOE = IOE;
            Health = new HealthComponent(health);
        }
        
        public void Initialize()
        {
            Damageable = new DamageableComponent(Health);

            IOE.OnDamageAttempt += Damageable.TakeDamage;
        }

        public void Cleanup()
        {
            IOE.OnDamageAttempt -= Damageable.TakeDamage;
        }
    }
}