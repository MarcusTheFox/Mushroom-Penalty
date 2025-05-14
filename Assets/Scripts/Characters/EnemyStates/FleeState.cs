using UnityEngine;

public class FleeState : IState
{
    private readonly EnemyStateMachine _stateMachine;
    private readonly Enemy _enemy;

    public FleeState(EnemyStateMachine enemyStateMachine, Enemy enemy)
    {
        _stateMachine = enemyStateMachine;
        _enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("Entering Flee State", _enemy);
    }

    public void Update()
    {
        if (_enemy.GetDistanceToPlayer() >= _enemy.StopMoveDistance)
        {
            _stateMachine.ChangeState(_stateMachine.AttackState);
            return;
        }
        
        _enemy.FleeFromPlayer();
    }

    public void Exit()
    {
        Debug.Log("Exiting Flee State", _enemy);
    }
}
