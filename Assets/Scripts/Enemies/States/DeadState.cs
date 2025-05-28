using AI;
using Enemies.CoreLogic;
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
        
        public override void OnEnter(Enemy context)
        {
            base.OnEnter(context);
            animationController.OnDie();
        }
    }
}