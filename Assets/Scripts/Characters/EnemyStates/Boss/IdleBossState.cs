using UnityEngine;

public class IdleBossState : IState
{
    private readonly BossStateMachine _stateMachine;
    private readonly Boss _boss;


    public IdleBossState(BossStateMachine stateMachine, Boss boss)
    {
        _stateMachine = stateMachine;
        _boss = boss;

        _boss.HealthSystem.OnHealthChanged += OnGetHit;
    }

    public void OnGetHit()
    {
        _stateMachine.ChangeState(_stateMachine.AgressiveBossState);
    }

    public void Enter()
    {
        Debug.Log("Entering Boss Idle State", _boss);
        _boss.StopMoving();
    }

    public void Update()
    {

    }

    public void Exit()
    {
        _boss.HealthSystem.OnHealthChanged -= OnGetHit;
        Debug.Log("Exiting Boss Idle State", _boss);
    }
}
