using UnityEngine;

public class StrongAttackBossState : IState
{
    private readonly BossStateMachine _stateMachine;
    private readonly Boss _boss;

    public StrongAttackBossState(BossStateMachine stateMachine, Boss boss)
    {
        _stateMachine = stateMachine;
        _boss = boss;
    }

    public void Enter()
    {
        Debug.Log("Entering StrongBossAttack State", _boss);
    }

    public void Update()
    {
        float distanceToPlayer = _boss.GetDistanceToPlayer();

        if (distanceToPlayer > _boss.AttackRange)
        {
            _stateMachine.ChangeState(_stateMachine.AgressiveBossState);
            return;
        }

        if (_stateMachine.AttackNumberCounter < 3)
        {
            _stateMachine.ChangeState(_stateMachine.AttackBossState);
            return;
        }

        if (_stateMachine.DamageNumberCounter >= 3)
        {
            _stateMachine.ResetDamageNumberCounter();
            _stateMachine.ChangeState(_stateMachine.ShieldBossState);
            return;
        }

        _boss.StrongAttackPlayer();
    }

    public void Exit()
    {
        Debug.Log("Exiting StrongBossAttack State", _boss);
    }
}
