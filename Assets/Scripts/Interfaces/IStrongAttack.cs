public interface IStrongAttack
{
    void PerformAttack();
    bool IsOnCooldown { get; }
    float GetCooldownProgress();
}
