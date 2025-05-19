using System;
using UnityEngine;

public interface IDamageable
{
    public IHealth HealthComponent { get; }
    public event Action<float> OnTakeDamage;
    public event Action OnDeath;
    
    public void TakeDamage(float damage);
    public void Death();
}
