public interface IAttacker
{
    void PerformAttack();
    bool IsOnCooldown { get; }
    float GetCooldownProgress();
}