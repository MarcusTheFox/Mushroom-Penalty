using AI;
using Combat.Interfaces;
using Core.Interfaces;
using Enemies.CoreLogic;
using Enemies.Handlers;

namespace Enemies.States.AttackStates
{
    public abstract class AttackState : BaseState, ICleanupable
    {
        protected readonly IAttack attack;
        protected readonly EnemyAnimationController animationController;
        public bool IsAttackSequenceComplete { get; private set; }

        public AttackState(IAttack attack, EnemyAnimationController animationController)
        {
            this.attack = attack;
            this.animationController = animationController;

            this.attack.OnReady += SetAttackIsReady;
        }

        public void Cleanup()
        {
            attack.OnReady -= SetAttackIsReady;
        }

        public override void OnEnter(Enemy context)
        {
            base.OnEnter(context);
            IsAttackSequenceComplete = false;
            context.LookToTarget();
            attack.Start();
            StartAnimation();
        }

        public override void OnUpdate(Enemy context, float deltaTime)
        {
            base.OnUpdate(context, deltaTime);
            context.LookToTarget();
        }
        
        protected abstract void StartAnimation();

        private void SetAttackIsReady()
        {
            IsAttackSequenceComplete = true;
        }
    }
}