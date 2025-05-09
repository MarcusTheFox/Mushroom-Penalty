using UnityEngine;

public class HealthSystem
{
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }

    public bool IsDead => CurrentHealth <= 0;
    
    public event System.Action OnHealthChanged;

    public HealthSystem(float maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        
        OnHealthChanged?.Invoke();
    }
    
    public void Heal(float amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        OnHealthChanged?.Invoke();
    }
}