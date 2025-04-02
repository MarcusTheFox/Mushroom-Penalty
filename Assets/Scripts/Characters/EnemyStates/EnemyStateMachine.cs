using UnityEngine;

public class EnemyStateMachine
{
    private readonly Enemy _enemy;

    public IState CurrentState { get; private set; }
    public IdleState IdleState { get; private set; }
    public MoveState MoveState { get; private set; }
    public AttackState AttackState { get; private set; }
    public FleeState FleeState { get; private set; }

    public EnemyStateMachine(Enemy enemy)
    {
        _enemy = enemy;

        IdleState = new IdleState(this, _enemy);
        MoveState = new MoveState(this, _enemy);
        AttackState = new AttackState(this, _enemy);
        FleeState = new FleeState(this, _enemy);
    }

    public void Initialize(IState startingState)
    {
        ChangeState(startingState);
        Debug.Log($"Initialized EnemyStateMachine with state: {startingState.GetType().Name}", _enemy);
    }

    public void Update()
    {
        CurrentState?.Update();
    }

    public void ChangeState(IState newState)
    {
        if (newState == null)
        {
            Debug.LogError("Cannot change state to null!", _enemy);
            return;
        }

        if (CurrentState == newState)
        {
            Debug.LogWarning($"Trying to change to the same state: {newState.GetType().Name}", _enemy);
            return;
        }
        
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }
}
