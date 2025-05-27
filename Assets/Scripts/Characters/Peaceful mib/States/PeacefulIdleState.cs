using UnityEngine;

public class PeacefulIdleState : IState
{
    private PeacefulMob mob;

    public PeacefulIdleState(PeacefulMob mob)
    {
        this.mob = mob;
    }

    public void Enter()
    {
        mob.animationController.PlayFleeAnimation(false);
    }

    public void Update()
    {
        // Ничего не делает
    }

    public void Exit()
    {
    }
}
