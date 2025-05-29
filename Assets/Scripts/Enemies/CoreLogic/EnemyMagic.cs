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
    public class EnemyMagic : Enemy
    {
        private readonly EnemyContext context;
        private readonly Transform fireballSpawnPoint;
        private readonly EnemyMagicDataSO data;
        private readonly Transform enemyTransform;
        private readonly Transform target;
        
        private EnemyMagicAttackSetup magicAttackSetup;
        private EnemyAnimationController animationController;
        private EnemyMagicAttackSetup attackSetup;
        private EnemyMovementTowards movementTowards;
        private EnemyMovementAway movementAway;

        public EnemyMagic(EnemyContext context, Transform fireballSpawnPoint, EnemyMagicDataSO data) : base(context)
        {
            this.context = context;
            this.fireballSpawnPoint = fireballSpawnPoint;
            this.data = data;
            enemyTransform = context.EnemyTransform;
            target = context.Target;
        }

        protected override void InitializeComponents()
        {
            animationController = new EnemyAnimationController(context.Animator);
            
            CoreComponents = new EnemyCoreComponentsSetup(context.IOE, data.health);
            CoreComponents.Initialize();
            
            movementTowards = new EnemyMovementTowards(context.EnemyTransform, data.speedTowards);
            movementAway = new EnemyMovementAway(context.EnemyTransform, data.speedAway);

            MagicSetupData magicSetupData = new ()
            {
                UEL = context.UEL,
                AEL = context.AEL,
                EnemyTransform = context.EnemyTransform,
                FireballSpawnPoint = fireballSpawnPoint,
                Cooldown = data.magicCooldown,
                Damage = data.magicDamage,
                ProjectilePrefab = data.magicProjectilePrefab,
                TargetLayer = data.targetLayer
            };
            magicAttackSetup = new EnemyMagicAttackSetup(magicSetupData);
            magicAttackSetup.Initialize();
            
            context.UI.Initialize(CoreComponents.Health);
        }

        protected override void ConfigureStateMachine()
        {
            var idleState = new IdleState();
            var chaseState = new ChaseState(movementTowards, animationController, target);
            var magicAttackState = new MagicAttackState(magicAttackSetup.MagicAttack, animationController, enemyTransform, target);
            var fleeState = new FleeState(movementAway, animationController, target);
            var deadState = new DeadState(animationController);

            stateMachine.AddState(idleState);
            stateMachine.AddState(chaseState);
            stateMachine.AddState(magicAttackState);
            stateMachine.AddState(fleeState);
            stateMachine.AddState(deadState);
            
            stateMachine.AddTransition<IdleState, ChaseState>(() => target && DistanceToTarget() < data.idleChaseRadius);
            stateMachine.AddTransition<ChaseState, IdleState>(() => target && DistanceToTarget() > data.chaseIdleRadius);
            stateMachine.AddTransition<ChaseState, MagicAttackState>(() => target && DistanceToTarget() < data.chaseAttackRadius);
            stateMachine.AddTransition<MagicAttackState, ChaseState>(() => magicAttackState.IsAttackSequenceComplete);
            stateMachine.AddTransition<MagicAttackState, FleeState>(() => target && DistanceToTarget() < data.attackFleeRadius);
            stateMachine.AddTransition<FleeState, MagicAttackState>(() => target && DistanceToTarget() > data.fleeAttackRadius);
            
            stateMachine.AddAnyTransition<IdleState>(() => !target && CoreComponents.Health.Health > 0f);
            stateMachine.AddAnyTransition<DeadState>(() => CoreComponents.Health.Health <= 0f);
            
            stateMachine.Initialize(idleState);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            CoreComponents.Cleanup();
            magicAttackSetup.Cleanup();
            
            context.UI.Cleanup();
        }
    }
}