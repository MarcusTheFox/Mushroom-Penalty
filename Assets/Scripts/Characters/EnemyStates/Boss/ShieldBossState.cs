using UnityEngine;

public class ShieldBossState : IState
{
    private readonly BossStateMachine _stateMachine;
    private readonly Boss _boss;
    private readonly HealthSystem _healthSystem;
    private float timer;

    public ShieldBossState(BossStateMachine stateMachine, Boss boss)
    {
        _stateMachine = stateMachine;
        _boss = boss;
        _healthSystem = boss.HealthSystem;
    }

    public void Enter()
    {
        Debug.Log("Entering ShieldAttack State", _boss);
        _healthSystem.Defense = true;
        timer = Time.time;
    }

    public void Update()
    {
        _healthSystem.Heal(_healthSystem.MaxHealth * 0.01f * Time.deltaTime);

        if (Time.time - timer >= Random.Range(5f, 10f))
        {
            _stateMachine.ChangeState(_stateMachine.KickAttackBossState);
        }
    }

    public void Exit()
    {
        _healthSystem.Defense = false;
        Debug.Log("Exiting ShieldAttack State", _boss);
    }
}
