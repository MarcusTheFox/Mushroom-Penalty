using UnityEngine;

public class AgressiveBossState : IState
{
    private readonly BossStateMachine _stateMachine;
    private readonly Boss _boss;
    private readonly HealthSystem _healthSystem;


    public AgressiveBossState(BossStateMachine stateMachine, Boss boss)
    {
        _stateMachine = stateMachine;
        _boss = boss;
        _healthSystem = boss.HealthSystem;
    }

    public void Enter()
    {
        Debug.Log("Entering Boss Agressive State", _boss);
        _boss.StopMoving();
    }

    public void Update()
    {
        float distanceToPlayer = _boss.GetDistanceToPlayer();

        if (distanceToPlayer <= _boss.StopMoveDistance)
        {
            _stateMachine.ChangeState(_stateMachine.AttackBossState);
            return;
        }

        if (!_stateMachine.HealingBossState.IsHealed && _healthSystem.CurrentHealth / _healthSystem.MaxHealth < 0.2)
        {
            _stateMachine.ChangeState(_stateMachine.HealingBossState);
            return;
        }

        if(_stateMachine.DamageNumberCounter >= 3)
        {
            _stateMachine.ResetDamageNumberCounter();
            _stateMachine.ChangeState(_stateMachine.ShieldBossState);
            return;
        }

        _boss.MoveToPlayer();
    }

    public void Exit()
    {
        Debug.Log("Exiting Boss Agressive State", _boss);
    }
}
