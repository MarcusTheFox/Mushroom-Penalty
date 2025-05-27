using UnityEngine;

public class PeacefulFleeState : IState
{
    private PeacefulMob mob;

    public PeacefulFleeState(PeacefulMob mob)
    {
        this.mob = mob;
    }

    private float minFleeTime = 1f;
    private float fleeTimer = 0f;

    public void Enter()
    {
        Debug.Log("Entered Flee");
        mob.LookAwayFromPlayer();
        fleeTimer = 0f;

        mob.animationController.PlayFleeAnimation(true); // Включаем анимацию бега
    }

    public void Update()
    {
        fleeTimer += Time.deltaTime;

        float distance = mob.GetDistanceToPlayer();
        Debug.Log($"FleeState Update, distance to player: {distance}");

        if (distance > mob.fleeDistance && fleeTimer > minFleeTime)
        {
            mob.stateMachine.ChangeState(StateType.Idle);
            return;
        }

        mob.FleeFromPlayer();
    }

    public void Exit()
    {
        mob.animationController.PlayFleeAnimation(false); // Отключаем анимацию бега
    }
}
