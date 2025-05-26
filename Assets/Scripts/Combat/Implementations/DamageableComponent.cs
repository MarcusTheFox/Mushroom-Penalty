using System;
using Combat.Interfaces;

namespace Combat.Implementations
{
    public class DamageableComponent: IDamageable
    {
        public IHealth HealthComponent { get; }
        public event Action<float> OnTakeDamage;
        public event Action OnDeath;

        public DamageableComponent(IHealth healthComponent)
        {
            HealthComponent = healthComponent;
        }
    
        public void TakeDamage(float damage)
        {
            HealthComponent.Decrease(damage);
            OnTakeDamage?.Invoke(damage);

            if (HealthComponent.Health <= 0f)
            {
                Death();
            }
        }

        public void Death()
        {
            OnDeath?.Invoke();
        }
    }
}
