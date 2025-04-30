using UnityEngine;

public class HealingBossState : IState
{
    private readonly BossStateMachine _stateMachine;
    private readonly Boss _boss;
    private readonly HealthSystem _healthSystem;

    public bool IsHealed { get; private set; }

    public HealingBossState(BossStateMachine stateMachine, Boss boss)
    {
        _stateMachine = stateMachine;
        _boss = boss;
        _healthSystem = boss.HealthSystem;
    }

    public void Enter()
    {
        Debug.Log("Entering Boss Healing State", _boss);
         
        if (!IsHealed)
        {
            _healthSystem.Heal(_healthSystem.MaxHealth * 0.3f);
            _stateMachine.ChangeState(_stateMachine.AgressiveBossState);
            IsHealed = true;
        }
    }

    public void Update()
    {

    }

    public void Exit()
    {
        Debug.Log("Exiting Boss Healing State", _boss);
    }
}
