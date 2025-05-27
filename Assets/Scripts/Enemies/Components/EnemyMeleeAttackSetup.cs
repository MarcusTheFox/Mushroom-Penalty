using Combat.Handlers;
using Combat.Implementations;
using Combat.Interfaces;
using Core.Interfaces;
using Core.UnityHooks;
using Enemies.Data;
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

        public IAttack MeleeAttack { get; private set; }
        private MeleeAttackAnimationEventHandler attackAnimationEventHandler;

        public EnemyMeleeAttackSetup(AnimationEventListener AEL, Transform target, EnemyMeleeDataSO data)
        {
            this.AEL = AEL;
            this.target = target;
            damage = data.meleeDamage;
            targetLayer = data.targetLayer;
            attackRadius = data.meleeRange;
            attackAngle = data.meleeAngle;
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