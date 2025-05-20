using Model;
using UnityEngine;

namespace Controller
{
    public class PlayerCoreComponentsSetup : ICleanupable
    {
        public IHealth Health { get; private set; }
        public IDamageable Damageable { get; private set; }

        private PlayerInputController PIC;

        public PlayerCoreComponentsSetup(PlayerInputController PIC)
        {
            this.PIC = PIC;
        }
        
        public void Initialize()
        {
            Health = new HealthComponent(100);
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