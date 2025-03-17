using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] protected float physicalDamage = 10f;
    [SerializeField] protected float magicalDamage = 15f;

    protected HealthSystem healthSystem;
    protected AnimationController animationController;

    protected virtual void Awake()
    {
        healthSystem = new HealthSystem(maxHealth);
        animationController = GetComponent<AnimationController>();
        if (animationController == null)
        {
            Debug.LogError("AnimationController not found on " + gameObject.name);
        }
    }

    public virtual void TakeDamage(float damage, DamageType type)
    {
        healthSystem.TakeDamage(damage);
        Debug.Log(healthSystem.CurrentHealth);
        animationController?.PlayDamageAnimation();
        if (healthSystem.IsDead)
        {
            Die();
        }
    }
    
    public void PlayAttackAnimation()
    {
        animationController?.PlayAttackAnimation();
    }
    
    public float GetCurrentHealth()
    {
        return healthSystem.CurrentHealth;
    }

    public float GetMaxHealth()
    {
        return healthSystem.MaxHealth;
    }

    protected abstract void Die();
}