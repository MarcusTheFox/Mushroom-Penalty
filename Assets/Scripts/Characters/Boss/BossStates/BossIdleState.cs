using UnityEngine;

public class BossIdleState : IState
{
    private Boss boss;
    private BossStateMachine machine;

    public BossIdleState(Boss boss, BossStateMachine machine)
    {
        this.boss = boss;
        this.machine = machine;
    }

    public void Enter()
    {
        Debug.Log("Босс в режиме ожидания");
    }

    public void Update()
    {
    }

    public void Exit() { }
}
