using UnityEngine;

public class AttackState : IState
{
    private readonly EnemyStateMachine _stateMachine;
    private readonly Enemy _enemy;

    public AttackState(EnemyStateMachine enemyStateMachine, Enemy enemy)
    {
        _stateMachine = enemyStateMachine;
        _enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("Entering Attack State", _enemy);
        _enemy.StopMoving();
    }

    public void Update()
    {
        float distanceToPlayer = _enemy.GetDistanceToPlayer();
        
        if (distanceToPlayer > _enemy.AttackRange)
        {
            _stateMachine.ChangeState(_stateMachine.MoveState);
            return;
        }

        if (distanceToPlayer < _enemy.FleeDistance)
        {
            _stateMachine.ChangeState(_stateMachine.FleeState);
            return;
        }
        
        _enemy.AttackPlayer();
    }

    public void Exit()
    {
        Debug.Log("Exiting Attack State", _enemy);
    }
}
