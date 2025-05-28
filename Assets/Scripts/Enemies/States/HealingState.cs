using AI;
using Combat.Interfaces;
using Enemies.CoreLogic;
using Enemies.Handlers;

namespace Enemies.States
{
    public class HealingState : BaseState
    {
        private readonly IHealth healthComponent;
        private readonly EnemyAnimationController animationController;
        private readonly float duration;
        private readonly float healPerSecond;
        private float timer;
        
        public bool IsHealingComplete { get; private set; }

        public HealingState(IHealth healthComponent, EnemyAnimationController animationController, float duration, float healPerSecond)
        {
            this.healthComponent = healthComponent;
            this.animationController = animationController;
            this.duration = duration;
            this.healPerSecond = healPerSecond;
        }

        public override void OnEnter(Enemy context)
        {
            base.OnEnter(context);
            IsHealingComplete = false;
            timer = 0f;
            animationController.OnBlock(true);
        }

        public override void OnUpdate(Enemy context, float deltaTime)
        {
            base.OnUpdate(context, deltaTime);
            if (IsHealingComplete) return;
            
            timer += deltaTime;
            if (timer < duration) healthComponent.Increase(healPerSecond * deltaTime);
            else IsHealingComplete = true;
        }

        public override void OnExit(Enemy context)
        {
            base.OnExit(context);
            animationController.OnBlock(false);
        }
    }
}