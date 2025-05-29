using Combat.Implementations;
using Core.Movement;
using Enemies.Components;
using Enemies.Components.Data;
using Enemies.CoreLogic.Context;
using Enemies.Data;
using Enemies.Handlers;
using Enemies.States;
using Enemies.States.AttackStates;
using UnityEngine;

namespace Enemies.CoreLogic
{
    public class Boss : Enemy
    {
        private readonly EnemyContext context;
        private readonly BossDataSO data;
        private readonly Transform enemyTransform;
        private readonly Transform target;
        
        private DamageableComponent damageableComponent;
        private BossCoreComponentsSetup coreSetup;
        private EnemyAnimationController animationController;
        private EnemyMovementTowards movementTowards;
        private EnemyMeleeAttackSetup attackSetup;
        private EnemyMeleeAttackSetup strongAttackSetup;
        private EnemyMeleeAttackSetup explosionAttackSetup;
        
        private int attackCounter;
        private bool isHealed;

        public Boss(EnemyContext context, BossDataSO data) : base(context)
        {
            this.context = context;
            this.data = data;
            enemyTransform = context.EnemyTransform;
            target = context.Target;
        }
        
        protected override void InitializeComponents()
        {
            animationController = new EnemyAnimationController(context.Animator);

            coreSetup = new BossCoreComponentsSetup(context.IOE, data.health);
            coreSetup.Initialize();

            MeleeSetupData attackSetupData = new ()
            {
                AEL = context.AEL,
                EnemyTransform = context.EnemyTransform,
                Damage = data.meleeDamage,
                TargetLayer = data.targetLayer,
                AttackRange = data.meleeRange,
                AttackAngle = data.meleeAngle
            };
            attackSetup = new EnemyMeleeAttackSetup(attackSetupData);
            attackSetup.Initialize();

            MeleeSetupData strongAttackSetupData = new ()
            {
                AEL = context.AEL,
                EnemyTransform = context.EnemyTransform,
                Damage = data.strongMeleeDamage,
                TargetLayer = data.targetLayer,
                AttackRange = data.strongMeleeRange,
                AttackAngle = data.strongMeleeAngle
            };
            strongAttackSetup = new EnemyMeleeAttackSetup(strongAttackSetupData);
            strongAttackSetup.Initialize();

            MeleeSetupData explosionAttackSetupData = new ()
            {
                AEL = context.AEL,
                EnemyTransform = context.EnemyTransform,
                Damage = data.explosionDamage,
                TargetLayer = data.targetLayer,
                AttackRange = data.explosionRadius,
                AttackAngle = 360f
            };
            explosionAttackSetup = new EnemyMeleeAttackSetup(explosionAttackSetupData);
            explosionAttackSetup.Initialize();
            
            movementTowards = new EnemyMovementTowards(context.EnemyTransform, data.speed);
            
            context.UI.Initialize(coreSetup.Health);
        }

        protected override void ConfigureStateMachine()
        {
            var idleState = new IdleState();
            var chaseState = new ChaseState(movementTowards, animationController, target);
            var meleeAttackState = new MeleeAttackState(attackSetup.MeleeAttack, animationController, enemyTransform, target);
            var strongMeleeAttackState = new StrongMeleeAttackState(strongAttackSetup.MeleeAttack, animationController, enemyTransform, target);
            var healingState = new HealingState(coreSetup.Health, animationController, data.blockHealDuration, data.healPerSecond);
            var explosionState = new ExplosionState(explosionAttackSetup.MeleeAttack, animationController, enemyTransform, target);
            var deadState = new DeadState(animationController);

            stateMachine.AddState(idleState);
            stateMachine.AddState(chaseState);
            stateMachine.AddState(meleeAttackState);
            stateMachine.AddState(strongMeleeAttackState);
            stateMachine.AddState(healingState);
            stateMachine.AddState(explosionState);
            stateMachine.AddState(deadState);
            
            stateMachine.AddTransition<IdleState, ChaseState>(() => target && DistanceToTarget() < data.idleChaseRadius);
            stateMachine.AddTransition<ChaseState, IdleState>(() => !target);
            stateMachine.AddTransition<ChaseState, MeleeAttackState>(
                () => target &&
                     DistanceToTarget() < data.chaseAttackRadius &&
                     attackCounter < data.attacksNumberForStrongAttack,
                () =>
                {
                    attackSetup.AddAttackAnimationEventHandler();
                    attackCounter++;
                });
            
            stateMachine.AddTransition<MeleeAttackState, ChaseState>(
                () => meleeAttackState.IsAttackSequenceComplete,
                () => attackSetup.RemoveAttackAnimationEventHandler());
            
            stateMachine.AddTransition<ChaseState, StrongMeleeAttackState>(
                () => target &&
                     DistanceToTarget() < data.chaseAttackRadius &&
                     attackCounter >= data.attacksNumberForStrongAttack,
                () =>
                {
                    strongAttackSetup.AddAttackAnimationEventHandler();
                    attackCounter = 0;
                });
            
            stateMachine.AddTransition<StrongMeleeAttackState, ChaseState>(
                () => strongMeleeAttackState.IsAttackSequenceComplete,
                () => strongAttackSetup.RemoveAttackAnimationEventHandler());
            
            stateMachine.AddAnyTransition<HealingState>(
                () => !isHealed &&
                     coreSetup.Health.Health / coreSetup.Health.MaxHealth < data.healthThresholdForHealing &&
                     stateMachine.CurrentState != deadState && 
                     stateMachine.CurrentState != explosionState,
                () =>
                {
                    isHealed = true;
                    attackSetup.RemoveAttackAnimationEventHandler();
                    strongAttackSetup.RemoveAttackAnimationEventHandler();
                    attackCounter = 0;
                });
            
            stateMachine.AddTransition<HealingState, ExplosionState>(
                () => healingState.IsHealingComplete,
                () => explosionAttackSetup.AddAttackAnimationEventHandler());
            
            stateMachine.AddTransition<ExplosionState, ChaseState>(
                () => explosionState.IsAttackSequenceComplete,
                () => explosionAttackSetup.RemoveAttackAnimationEventHandler());
            
            stateMachine.AddAnyTransition<IdleState>(() => !target && coreSetup.Health.Health > 0f);
            stateMachine.AddAnyTransition<DeadState>(() => coreSetup.Health.Health <= 0f);
            
            stateMachine.Initialize(idleState);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            coreSetup.Cleanup();
            attackSetup.Cleanup();
            
            context.UI.Cleanup();
        }
    }
}