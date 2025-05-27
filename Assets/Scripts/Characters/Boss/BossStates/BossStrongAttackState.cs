using UnityEngine;

public class BossStrongAttackState : IState
{
    private Boss boss;
    private BossStateMachine machine;

    public BossStrongAttackState(Boss boss, BossStateMachine machine)
    {
        this.boss = boss;
        this.machine = machine;
    }

    public void Enter()
    {
        Debug.Log("Босс выполняет сильную атаку");

        boss.animationController.PlayBossStrongAttackAnimation();
    }

    public void Update()
    {
        if (boss.IsPlayerDead())
        {
            boss.StopMoving();
            boss.animationController?.DontPlayBossStrongAttackAnimation();
            machine.ChangeState(StateType.Idle);
            return;
        }
    }

    public void Exit()
    {
        Debug.Log("Босс завершил сильную атаку");

    }
}
