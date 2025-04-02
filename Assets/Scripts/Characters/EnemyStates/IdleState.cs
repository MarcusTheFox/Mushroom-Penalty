using UnityEngine;

public class IdleState : IState
{
    private readonly EnemyStateMachine _stateMachine;
    private readonly Enemy _enemy;

    public IdleState(EnemyStateMachine stateMachine, Enemy enemy)
    {
        _stateMachine = stateMachine;
        _enemy = enemy;
    }
    
    public void Enter()
    {
        Debug.Log("Entering Idle State", _enemy);
        _enemy.StopMoving();
    }

    public void Update()
    {
        if (_enemy.GetDistanceToPlayer() < _enemy.ViewDistance)
        {
            _stateMachine.ChangeState(_stateMachine.MoveState);
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Idle State", _enemy);
    }
}
