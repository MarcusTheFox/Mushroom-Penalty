using System;
using UnityEngine;

public class HealthComponent: IHealth
{
    private float health;
    
    public float Health
    {
        get => health;
        private set => health = Mathf.Clamp(value, 0, MaxHealth);
    }

    public float MaxHealth { get; }
    
    public event Action<float> OnChange;
    public event Action<float> OnIncrease;
    public event Action<float> OnDecrease;

    public HealthComponent(float maxHealth)
    {
        MaxHealth = maxHealth;
        Health = maxHealth;
    }

    public HealthComponent(float health, float maxHealth)
    {
        MaxHealth = maxHealth;
        Health = health;
    }
    
    public void Increase(float amount)
    {
        Health += amount;
        OnIncrease?.Invoke(amount);
        OnChange?.Invoke(Health);
    }

    public void Decrease(float amount)
    {
        Health -= amount;
        OnDecrease?.Invoke(amount);
        OnChange?.Invoke(Health);
    }
}
