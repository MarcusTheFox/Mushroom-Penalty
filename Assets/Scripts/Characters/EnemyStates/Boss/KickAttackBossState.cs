using UnityEngine;

public class KickAttackBossState : IState
{
    private readonly BossStateMachine _stateMachine;
    private readonly Boss _boss;

    public KickAttackBossState(BossStateMachine stateMachine, Boss boss)
    {
        _stateMachine = stateMachine;
        _boss = boss;
    }

    public void Enter()
    {
        Debug.Log("Entering KickBossAttack State", _boss);
        
    }

    public void Update()
    {

    }

    public void Exit()
    {
        Debug.Log("Exiting KickBossAttack State", _boss);
    }
}
