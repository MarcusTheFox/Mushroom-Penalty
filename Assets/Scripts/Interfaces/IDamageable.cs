public interface IDamageable
{
    void TakeDamage(float damage, DamageType type);
    void PlayAttackAnimation();
    float GetCurrentHealth();
    float GetMaxHealth();
}