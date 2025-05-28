using Combat.Implementations;
using Core.Interfaces;
using Core.Movement;
using Core.UnityHooks;
using Enemies.Components;
using Enemies.Components.Data;
using Enemies.Data;
using Enemies.Handlers;
using Enemies.States;
using Enemies.States.AttackStates;
using Enemies.UI;
using UnityEngine;

namespace Enemies.CoreLogic
{
    public class Boss : Enemy, IConfigurable<BossDataSO>
    {
        private BossDataSO data;
        private DamageableComponent damageableComponent;
        private BossCoreComponentsSetup coreSetup;
        private EnemyAnimationController animationController;
        private EnemyMovementTowards movementTowards;
        private EnemyMeleeAttackSetup attackSetup;
        private EnemyMeleeAttackSetup strongAttackSetup;
        private EnemyMeleeAttackSetup explosionAttackSetup;
        
        private int attackCounter;
        private bool isHealed;

        public Boss(UnityEventListener UEL,
            AnimationEventListener AEL,
            InteractableObjectEvents IOE,
            EnemyUI UI,
            Transform enemyTransform,
            Animator animator,
            Transform target) : base(UEL,
            AEL,
            IOE,
            UI,
            enemyTransform,
            animator,
            target)
        {
        }

        public void Configure(BossDataSO data)
        {
            this.data = data;
        }

        protected override void InitializeComponents()
        {
            animationController = new EnemyAnimationController(animator);

            coreSetup = new BossCoreComponentsSetup(IOE, data.health);
            coreSetup.Initialize();

            MeleeSetupData attackSetupData = new ()
            {
                AEL = AEL,
                EnemyTransform = EnemyTransform,
                Damage = data.meleeDamage,
                TargetLayer = data.targetLayer,
                AttackRange = data.meleeRange,
                AttackAngle = data.meleeAngle
            };
            attackSetup = new EnemyMeleeAttackSetup(attackSetupData);
            attackSetup.Initialize();

            MeleeSetupData strongAttackSetupData = new ()
            {
                AEL = AEL,
                EnemyTransform = EnemyTransform,
                Damage = data.strongMeleeDamage,
                TargetLayer = data.targetLayer,
                AttackRange = data.strongMeleeRange,
                AttackAngle = data.strongMeleeAngle
            };
            strongAttackSetup = new EnemyMeleeAttackSetup(strongAttackSetupData);
            strongAttackSetup.Initialize();

            MeleeSetupData explosionAttackSetupData = new ()
            {
                AEL = AEL,
                EnemyTransform = EnemyTransform,
                Damage = data.explosionDamage,
                TargetLayer = data.targetLayer,
                AttackRange = data.explosionRadius,
                AttackAngle = 360f
            };
            explosionAttackSetup = new EnemyMeleeAttackSetup(explosionAttackSetupData);
            explosionAttackSetup.Initialize();
            
            movementTowards = new EnemyMovementTowards(EnemyTransform, data.speed);
            
            UI.Initialize(coreSetup.Health);
        }

        protected override void ConfigureStateMachine()
        {
            var idleState = new IdleState();
            var chaseState = new ChaseState(movementTowards, animationController, Target);
            var meleeAttackState = new MeleeAttackState(attackSetup.MeleeAttack, animationController, EnemyTransform, Target);
            var strongMeleeAttackState = new StrongMeleeAttackState(strongAttackSetup.MeleeAttack, animationController, EnemyTransform, Target);
            var healingState = new HealingState(coreSetup.Health, animationController, data.blockHealDuration, data.healPerSecond);
            var explosionState = new ExplosionState(explosionAttackSetup.MeleeAttack, animationController, EnemyTransform, Target);
            var deadState = new DeadState(animationController);

            stateMachine.AddState(idleState);
            stateMachine.AddState(chaseState);
            stateMachine.AddState(meleeAttackState);
            stateMachine.AddState(strongMeleeAttackState);
            stateMachine.AddState(healingState);
            stateMachine.AddState(explosionState);
            stateMachine.AddState(deadState);
            
            stateMachine.AddTransition<IdleState, ChaseState>(() => Target && DistanceToTarget() < data.idleChaseRadius);
            stateMachine.AddTransition<ChaseState, IdleState>(() => !Target);
            stateMachine.AddTransition<ChaseState, MeleeAttackState>(
                () => Target &&
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
                () => Target &&
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
            
            stateMachine.AddAnyTransition<IdleState>(() => !Target && coreSetup.Health.Health > 0f);
            stateMachine.AddAnyTransition<DeadState>(() => coreSetup.Health.Health <= 0f);
            
            stateMachine.Initialize(idleState);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            coreSetup.Cleanup();
            attackSetup.Cleanup();
            
            UI.Cleanup();
        }
    }
}