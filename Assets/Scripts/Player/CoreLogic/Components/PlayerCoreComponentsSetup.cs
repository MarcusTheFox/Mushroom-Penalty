using Combat.Implementations;
using Combat.Interfaces;
using Core.Interfaces;
using Core.UnityHooks;
using Player.Input;

namespace Player.CoreLogic.Components
{
    public class PlayerCoreComponentsSetup : ICleanupable
    {
        public IHealth Health { get; private set; }
        public IDamageable Damageable { get; private set; }

        private PlayerInputController PIC;
        private readonly InteractableObjectEvents IOE;

        public PlayerCoreComponentsSetup(PlayerInputController PIC, InteractableObjectEvents IOE, float health)
        {
            this.PIC = PIC;
            this.IOE = IOE;
            Health = new HealthComponent(health);
        }
        
        public void Initialize()
        {
            Damageable = new DamageableComponent(Health);

            IOE.OnDamageAttempt += Damageable.TakeDamage;
            Damageable.OnDeath += OnDeath;
        }

        public void Cleanup()
        {
            IOE.OnDamageAttempt -= Damageable.TakeDamage;
            Damageable.OnDeath -= OnDeath;
        }

        private void OnDeath()
        {
            IOE.OnDamageAttempt -= Damageable.TakeDamage;
            PIC.SetMovementInputEnabled(false);
            PIC.SetMeleeAttackInputEnabled(false);
            PIC.SetMagicAttackInputEnabled(false);
        }
    }
}