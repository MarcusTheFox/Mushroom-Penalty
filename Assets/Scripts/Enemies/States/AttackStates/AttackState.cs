using AI;
using Combat.Interfaces;
using Core.Interfaces;
using Enemies.Handlers;
using UnityEngine;

namespace Enemies.States.AttackStates
{
    public abstract class AttackState : BaseState, ICleanupable
    {
        protected readonly IAttack attack;
        protected readonly EnemyAnimationController animationController;
        private readonly Transform enemyTransform;
        private readonly Transform target;
        public bool IsAttackSequenceComplete { get; private set; }

        public AttackState(IAttack attack, EnemyAnimationController animationController, Transform enemyTransform, Transform target)
        {
            this.attack = attack;
            this.animationController = animationController;
            this.enemyTransform = enemyTransform;
            this.target = target;

            this.attack.OnReady += SetAttackIsReady;
        }

        public void Cleanup()
        {
            attack.OnReady -= SetAttackIsReady;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            IsAttackSequenceComplete = false;
            LookToTarget();
            attack.Start();
            StartAnimation();
        }

        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
            LookToTarget();
        }
        
        protected abstract void StartAnimation();

        private void SetAttackIsReady()
        {
            IsAttackSequenceComplete = true;
        }

        public void LookAt(Vector3 targetPoint)
        {
            Vector3 lookTarget = new Vector3(targetPoint.x, enemyTransform.position.y, targetPoint.z);
            enemyTransform.LookAt(lookTarget);
        }
        
        public void LookToTarget()
        {
            LookAt(target.position);
        }
    }
}