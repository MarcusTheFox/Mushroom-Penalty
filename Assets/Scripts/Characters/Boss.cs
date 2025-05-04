using UnityEngine;

public class Boss : Enemy
{
    private IStrongAttack strongAttack;
    [SerializeField] private KickAttack kickAttack; ///�� ������ ������� ���������� ��������� ��� ���� ���������� ��������� ���, ���� �� �������� ��� ���� ����


    public override void SetStateMachine()
    {
        stateMachine = new BossStateMachine(this);
        stateMachine.Initialize(((BossStateMachine)stateMachine).IdleBossState);
        strongAttack = GetComponent<IStrongAttack>();
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

    public void PlayChargeAnimation()
    {
        animationController.PlayChargeAnimation();
    }
    
    public void StrongAttackEndEvent()
    {
        canAttack = true;
        ((BossStateMachine)stateMachine).ResetAttackNumberCounter();
        EnableMovement();
    }
}
