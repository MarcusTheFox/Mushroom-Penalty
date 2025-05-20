using Model;
using UnityEngine;

namespace Controller
{
    public class PlayerCoreComponentsSetup : ICleanupable
    {
        public IHealth Health { get; private set; }
        public IDamageable Damageable { get; private set; }

        private PlayerInputController PIC;

        public PlayerCoreComponentsSetup(PlayerInputController PIC, float health)
        {
            this.PIC = PIC;
            Health = new HealthComponent(health);
        }
        
        public void Initialize()
        {
            Damageable = new DamageableComponent(Health);
            
            Damageable.OnDeath += OnDeath;
        }

        public void Cleanup()
        {
            Damageable.OnDeath -= OnDeath;
        }

        private void OnDeath()
        {
            PIC.SetMovementInputEnabled(false);
            PIC.SetMeleeAttackInputEnabled(false);
            PIC.SetMagicAttackInputEnabled(false);
        }
    }
}