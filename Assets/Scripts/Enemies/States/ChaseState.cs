using AI;
using Core.Interfaces;
using Enemies.CoreLogic;

namespace Enemies.States
{
    public class ChaseState : BaseState
    {
        private readonly IMovement movement;

        public ChaseState(IMovement movement)
        {
            this.movement = movement;
        }
        
        public override void OnUpdate(EnemyMelee context, float deltaTime)
        {
            base.OnUpdate(context, deltaTime);
            movement.Move(context.Target.position);
        }
    }
}