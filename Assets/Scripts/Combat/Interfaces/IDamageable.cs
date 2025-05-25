using System;

namespace Combat.Interfaces
{
    public interface IDamageable
    {
        public IHealth HealthComponent { get; }
        public event Action<float> OnTakeDamage;
        public event Action OnDeath;
    
        public void TakeDamage(float damage);
        public void Death();
    }
}
