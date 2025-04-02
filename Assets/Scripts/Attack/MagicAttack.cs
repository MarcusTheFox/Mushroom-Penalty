public abstract class MagicAttack : BaseAttack
{
    public override void PerformAttack()
    {
        if (IsOnCooldown) return;
        
        cooldownSystem.StartCooldown();
        
        animationController.PlayMagicAttackAnimation();
    }

    public virtual void MagicAttackEvent() { }
}
