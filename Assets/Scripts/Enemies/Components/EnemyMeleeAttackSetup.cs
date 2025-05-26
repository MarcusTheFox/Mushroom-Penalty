using Combat.Implementations;
using Combat.Interfaces;
using Core.Interfaces;
using Core.UnityHooks;
using Enemies.Handlers;
using UnityEngine;

namespace Enemies.Components
{
    public class EnemyMeleeAttackSetup : ICleanupable
    {
        private readonly AnimationEventListener AEL;
        private readonly float damage;
        private readonly Transform target;
        private readonly LayerMask targetLayer;
        private readonly float attackRadius;
        private readonly float attackAngle;
        private readonly Animator animator;

        public IAttack MeleeAttack { get; private set; }
        private MeleeAttackAnimationEventHandler attackAnimationEventHandler;

        public EnemyMeleeAttackSetup(AnimationEventListener AEL,
            float damage,
            Transform target,
            LayerMask targetLayer,
            float attackRadius,
            float attackAngle)
        {
            this.AEL = AEL;
            this.damage = damage;
            this.target = target;
            this.targetLayer = targetLayer;
            this.attackRadius = attackRadius;
            this.attackAngle = attackAngle;
        }

        public void Initialize()
        {
            MeleeAttack = new MeleeAttack(damage, target, targetLayer, attackRadius, attackAngle);

            attackAnimationEventHandler = new MeleeAttackAnimationEventHandler(MeleeAttack);
            
            AddAttackAnimationEventHandler();
        }

        public void Cleanup()
        {
            RemoveAttackAnimationEventHandler();
        }

        private void AddAttackAnimationEventHandler()
        {
            AEL.OnApplyMeleeAttack += attackAnimationEventHandler.ApplyMeleeAttack;
            AEL.OnStopMeleeAttack += attackAnimationEventHandler.StopMeleeAttack;
        }

        private void RemoveAttackAnimationEventHandler()
        {
            AEL.OnApplyMeleeAttack -= attackAnimationEventHandler.ApplyMeleeAttack;
            AEL.OnStopMeleeAttack -= attackAnimationEventHandler.StopMeleeAttack;
        }
    }
}