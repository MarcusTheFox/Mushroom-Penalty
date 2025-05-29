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
    public class EnemyMelee : Enemy
    {
        private readonly EnemyContext context;
        private readonly EnemyMeleeDataSO data;
        private readonly Transform enemyTransform;
        private readonly Transform target;
        
        private EnemyCoreComponentsSetup coreSetup;
        private EnemyAnimationController animationController;
        private EnemyMeleeAttackSetup attackSetup;
        private EnemyMovementTowards movementTowards;

        private IdleState idleState;
        private ChaseState chaseState;
        private MeleeAttackState meleeAttackState;
        private DeadState deadState;

        public EnemyMelee(EnemyContext context, EnemyMeleeDataSO data) : base(context)
        {
            this.context = context;
            this.data = data;
            enemyTransform = context.EnemyTransform;
            target = context.Target;
        }

        protected override void InitializeComponents()
        {
            animationController = new EnemyAnimationController(context.Animator);
            
            coreSetup = new EnemyCoreComponentsSetup(context.IOE, data.health);
            coreSetup.Initialize();

            MeleeSetupData attackData = new()
            {
                AEL = context.AEL,
                EnemyTransform = context.EnemyTransform,
                Damage = data.meleeDamage,
                TargetLayer = data.targetLayer,
                AttackRange = data.meleeRange,
                AttackAngle = data.meleeAngle,
            };
            attackSetup = new EnemyMeleeAttackSetup(attackData);
            attackSetup.Initialize();
            attackSetup.AddAttackAnimationEventHandler();
            
            movementTowards = new EnemyMovementTowards(enemyTransform, data.speed);
            
            context.UI.Initialize(coreSetup.Health);
        }

        protected override void ConfigureStateMachine()
        {
            idleState = new IdleState();
            chaseState = new ChaseState(movementTowards, animationController, target);
            meleeAttackState = new MeleeAttackState(attackSetup.MeleeAttack, animationController, enemyTransform, target);
            deadState = new DeadState(animationController);
            
            stateMachine.AddState(idleState);
            stateMachine.AddState(chaseState);
            stateMachine.AddState(meleeAttackState);
            stateMachine.AddState(deadState);
            
            stateMachine.AddTransition<IdleState, ChaseState>(() => target && DistanceToTarget() < data.idleChaseRadius);
            stateMachine.AddTransition<ChaseState, IdleState>(() => target && DistanceToTarget() > data.chaseIdleRadius);
            stateMachine.AddTransition<ChaseState, MeleeAttackState>(() => target && DistanceToTarget() < data.chaseAttackRadius);
            stateMachine.AddTransition<MeleeAttackState, ChaseState>(() => meleeAttackState.IsAttackSequenceComplete);
            
            stateMachine.AddAnyTransition<IdleState>(() => !target && coreSetup.Health.Health > 0f);
            stateMachine.AddAnyTransition<DeadState>(() => coreSetup.Health.Health <= 0f);
            
            stateMachine.Initialize(idleState);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            coreSetup.Cleanup();
            attackSetup.Cleanup();
            meleeAttackState.Cleanup();
            
            context.UI.Cleanup();
        }
    }
}