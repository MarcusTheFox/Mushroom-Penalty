using UnityEngine;

public class Boss : Enemy
{
    private IStrongAttack strongAttack;
    [SerializeField] private KickAttack kickAttack; ///не забыть сделать нормальный интерфейс или ваще переделать интерфейс так, чтоб он подходил для всех атак


    public override void SetStateMachine()
    {
        stateMachine = new BossStateMachine(this);
        stateMachine.Initialize(((BossStateMachine)stateMachine).IdleBossState);
    }

    public override void AttackPlayer()
    {
        LookToPlayer();

        if (canAttack)
        {
            ((BossStateMachine)stateMachine).IncreaseAttackNumberCounter();

            attack.PerformAttack();

            canAttack = false;
        }
    }

    public void StrongAttackPlayer()
    {
        LookToPlayer();

        if (canAttack)
        {
            strongAttack.PerformAttack();

            canAttack = false;
        }
    }

    public void KickAttackPlayer()
    {
        LookToPlayer();

        if (canAttack)
        {
            kickAttack.PerformAttack(); 

            canAttack = false;
        }
    }
}
