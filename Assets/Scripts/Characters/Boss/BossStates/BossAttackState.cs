using UnityEngine;

public class BossAttackState : IState
{
    private Boss boss;
    private BossStateMachine machine;

    public BossAttackState(Boss boss, BossStateMachine machine)
    {
        this.boss = boss;
        this.machine = machine;
    }

    public void Enter()
    {
        Debug.Log("Босс атакует");


        boss.animationController.PlayBossAttackAnimation(true);

        if (boss.normalAttackCount >= boss.strongAttackThreshold)
        {
            boss.normalAttackCount = 0;
            machine.ChangeState(StateType.StrongAttack);
        }
    }

    public void Update() 
    {
        if (boss.IsPlayerDead())
        {
            boss.StopMoving();
            boss.animationController?.PlayBossAttackAnimation(false);
            machine.ChangeState(StateType.Idle);
            return;
        }
    }

    public void Exit()
    {
        Debug.Log("Босс завершил обычную атаку");
        boss.animationController.PlayBossAttackAnimation(false);
    }
}
