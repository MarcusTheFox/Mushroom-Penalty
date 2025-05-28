using AI;
using Enemies.Handlers;

namespace Enemies.States
{
    public class DeadState : BaseState
    {
        private readonly EnemyAnimationController animationController;

        public DeadState(EnemyAnimationController animationController)
        {
            this.animationController = animationController;
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            animationController.OnDie();
        }
    }
}