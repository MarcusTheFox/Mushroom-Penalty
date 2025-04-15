using UnityEngine;

public class MoveState : IState
{
    private readonly EnemyStateMachine _stateMachine;
    private readonly Enemy _enemy;

    public MoveState(EnemyStateMachine enemyStateMachine, Enemy enemy)
    {
        _stateMachine = enemyStateMachine;
        _enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("Entering Move State", _enemy);
        
    }

    public void Update()
    {
        float distanceToPlayer = _enemy.GetDistanceToPlayer();
        
        if (distanceToPlayer <= _enemy.StopMoveDistance)
        {
            _stateMachine.ChangeState(_stateMachine.AttackState);
            return;
        }
        
        if (distanceToPlayer > _enemy.ViewDistance)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
            return;
        }
        
        _enemy.MoveToPlayer();
    }

    public void Exit()
    {
        Debug.Log("Exiting Move State", _enemy);
    }
}
