using Combat.Implementations;
using Combat.Interfaces;
using Core.UnityHooks;

namespace Enemies.Components
{
    public class BossCoreComponentsSetup
    {
        private readonly InteractableObjectEvents IOE;
        public IHealth Health { get; private set; }
        public IDamageable Damageable { get; private set; }

        public BossCoreComponentsSetup(InteractableObjectEvents IOE, float health)
        {
            this.IOE = IOE;
            Health = new HealthComponent(health);
        }
        
        public void Initialize()
        {
            Damageable = new DamageableComponent(Health);
            EnableTakeDamage();
        }

        public void Cleanup()
        {
            DisableTakeDamage();
        }

        public void EnableTakeDamage()
        {
            IOE.OnDamageAttempt += Damageable.TakeDamage;
        }

        public void DisableTakeDamage()
        {
            IOE.OnDamageAttempt -= Damageable.TakeDamage;
        }
    }
}