public interface IDamageable
{
    void TakeDamage(float damage, DamageType type);
    float GetCurrentHealth();
    float GetMaxHealth();
    HealthSystem HealthSystem { get; }
}