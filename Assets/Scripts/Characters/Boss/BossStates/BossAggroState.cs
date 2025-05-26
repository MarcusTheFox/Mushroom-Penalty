using UnityEngine;

public class BossAggroState : IState
{
    private Boss boss;
    private BossStateMachine machine;

    public BossAggroState(Boss boss, BossStateMachine machine)
    {
        this.boss = boss;
        this.machine = machine;
    }

    public void Enter()
    {
        Debug.Log("Босс в состоянии агрессии");
        boss.animationController.PlayBossAggroAnimation(true); // если есть
        boss.StartMoving();
    }

    public void Update()
    {
        boss.MoveToPlayer();

        if (boss.player != null)
        {
            float distance = Vector3.Distance(boss.transform.position, boss.player.position);
            if (distance <= boss.aggroRange)
            {
                machine.ChangeState(StateType.Attack);
            }
        }
    }

    public void Exit()
    {
        Debug.Log("Босс покидает состояние агрессии");
        boss.animationController.PlayBossAggroAnimation(false);
        boss.StopMoving();
    }
}


