public abstract class MagicAttack : BaseAttack
{
    public override void PerformAttack()
    {
        base.PerformAttack();
        
        animationController.PlayMagicAttackAnimation();
    }

    public virtual void MagicAttackEvent() { }
}
