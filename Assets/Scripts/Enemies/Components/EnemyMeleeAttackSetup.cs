using Combat.Handlers;
using Combat.Implementations;
using Combat.Interfaces;
using Core.Interfaces;
using Core.UnityHooks;
using Enemies.Components.Data;
using UnityEngine;

namespace Enemies.Components
{
    public class EnemyMeleeAttackSetup : ICleanupable
    {
        private readonly AnimationEventListener AEL;
        private readonly float damage;
        private readonly Transform fromTransform;
        private readonly LayerMask targetLayer;
        private readonly float attackRadius;
        private readonly float attackAngle;

        public IAttack MeleeAttack { get; private set; }
        private MeleeAttackAnimationEventHandler attackAnimationEventHandler;

        public EnemyMeleeAttackSetup(MeleeSetupData data)
        {
            AEL = data.AEL;
            fromTransform = data.EnemyTransform;
            damage = data.Damage;
            targetLayer = data.TargetLayer;
            attackRadius = data.AttackRange;
            attackAngle = data.AttackAngle;
        }

        public void Initialize()
        {
            MeleeAttack = new MeleeAttack(damage, fromTransform, targetLayer, attackRadius, attackAngle);
            attackAnimationEventHandler = new MeleeAttackAnimationEventHandler(MeleeAttack);
        }

        public void Cleanup()
        {
            RemoveAttackAnimationEventHandler();
        }

        public void AddAttackAnimationEventHandler()
        {
            AEL.OnApplyMeleeAttack += attackAnimationEventHandler.ApplyMeleeAttack;
            AEL.OnStopMeleeAttack += attackAnimationEventHandler.StopMeleeAttack;
        }

        public void RemoveAttackAnimationEventHandler()
        {
            AEL.OnApplyMeleeAttack -= attackAnimationEventHandler.ApplyMeleeAttack;
            AEL.OnStopMeleeAttack -= attackAnimationEventHandler.StopMeleeAttack;
        }
    }
}