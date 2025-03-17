public abstract class Attack
{
    protected float damage;
    protected IDamageable attacker;

    public Attack(IDamageable attacker, float damage)
    {
        this.attacker = attacker;
        this.damage = damage;
    }

    public abstract void Execute();
}