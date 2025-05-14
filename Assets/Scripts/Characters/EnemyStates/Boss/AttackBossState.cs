using UnityEngine;

public class AttackBossState : IState
{
    private readonly BossStateMachine _stateMachine;
    private readonly Boss _boss;

    public AttackBossState(BossStateMachine stateMachine, Boss boss)
    {
        _stateMachine = stateMachine;
        _boss = boss;
    }

    public void Enter()
    {
        Debug.Log("Entering BossAttack State", _boss);
    }

    public void Update()
    {
        float distanceToPlayer = _boss.GetDistanceToPlayer();

        if (distanceToPlayer > _boss.AttackRange)
        {
            _stateMachine.ChangeState(_stateMachine.AgressiveBossState);
            return;
        }

        if (_stateMachine.AttackNumberCounter >= 3)
        {
            _stateMachine.ResetAttackNumberCounter();
            _stateMachine.ChangeState(_stateMachine.StrongAttackBossState);
            return;
        }

        if (_stateMachine.DamageNumberCounter >= 3)
        {
            _stateMachine.ResetDamageNumberCounter();
            _stateMachine.ChangeState(_stateMachine.ShieldBossState);
            return;
        }

        _boss.AttackPlayer();
    }

    public void Exit()
    {
        Debug.Log("Exiting BossAttack State", _boss);
    }
}
