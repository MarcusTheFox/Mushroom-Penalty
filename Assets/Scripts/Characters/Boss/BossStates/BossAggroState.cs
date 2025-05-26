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
        Debug.Log("Ѕосс в состо€нии агрессии");
        boss.animationController.PlayBossAggroAnimation(true); // если есть
    }

    public void Update()
    {
        if (boss.player != null)
        {
            float distance = Vector3.Distance(boss.transform.position, boss.player.position);
            if (distance <= boss.aggroRange)
            {
                machine.ChangeState(StateType.Attack);
            }
            else
            {
                // «десь можно добавить логику движени€ к игроку, если хочешь
                // например: boss.MoveTowardsPlayer();
            }
        }
    }

    public void Exit()
    {
        Debug.Log("Ѕосс покидает состо€ние агрессии");
        boss.animationController.PlayBossAggroAnimation(false);
    }
}


