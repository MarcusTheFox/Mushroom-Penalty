using UnityEngine;

public class HealthSystem
{
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }
    public bool Defense { get; set; }

    public bool IsDead => CurrentHealth <= 0;
    
    public event System.Action OnHealthChanged;



    public HealthSystem(float maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (Defense)
        {
            CurrentHealth -= 1;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

            OnHealthChanged?.Invoke();
        }
        else
        {
            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

            OnHealthChanged?.Invoke();
        }

    }
    
    public void Heal(float amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        OnHealthChanged?.Invoke();
    }

    
}