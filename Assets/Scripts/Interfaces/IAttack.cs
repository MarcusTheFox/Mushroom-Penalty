public interface IAttack
{
    void PerformAttack();
    bool IsOnCooldown { get; }
    float GetCooldownProgress();
}