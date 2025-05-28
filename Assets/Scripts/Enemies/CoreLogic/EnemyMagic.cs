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
    public class EnemyMagic : Enemy, IConfigurable<EnemyMagicDataSO>
    {
        private readonly Transform fireballSpawnPoint;
        
        private EnemyMagicAttackSetup magicAttackSetup;
        private EnemyAnimationController animationController;
        private EnemyCoreComponentsSetup coreSetup;
        private EnemyMagicAttackSetup attackSetup;
        private EnemyMovementTowards movementTowards;
        private EnemyMovementAway movementAway;
        private EnemyMagicDataSO data;
        
        private IdleState idleState;
        private ChaseState chaseState;
        private MagicAttackState magicAttackState;
        private FleeState fleeState;
        private DeadState deadState;

        public EnemyMagic(UnityEventListener UEL,
            AnimationEventListener AEL,
            InteractableObjectEvents IOE,
            EnemyUI UI,
            Transform enemyTransform,
            Animator animator,
            Transform target,
            Transform fireballSpawnPoint) : base(UEL, AEL, IOE, UI, enemyTransform, animator, target)
        {
            this.fireballSpawnPoint = fireballSpawnPoint;
        }

        public void Configure(EnemyMagicDataSO data)
        {
            this.data = data;
        }

        protected override void InitializeComponents()
        {
            animationController = new EnemyAnimationController(animator);
            
            coreSetup = new EnemyCoreComponentsSetup(IOE, data.health);
            coreSetup.Initialize();
            
            movementTowards = new EnemyMovementTowards(EnemyTransform, data.speedTowards);
            movementAway = new EnemyMovementAway(EnemyTransform, data.speedAway);

            MagicSetupData magicSetupData = new ()
            {
                UEL = UEL,
                AEL = AEL,
                EnemyTransform = EnemyTransform,
                FireballSpawnPoint = fireballSpawnPoint,
                Cooldown = data.magicCooldown,
                Damage = data.magicDamage,
                ProjectilePrefab = data.magicProjectilePrefab,
                TargetLayer = data.targetLayer
            };
            magicAttackSetup = new EnemyMagicAttackSetup(magicSetupData);
            magicAttackSetup.Initialize();
            
            UI.Initialize(coreSetup.Health);
        }

        protected override void ConfigureStateMachine()
        {
            idleState = new IdleState();
            chaseState = new ChaseState(movementTowards, animationController, Target);
            magicAttackState = new MagicAttackState(magicAttackSetup.MagicAttack, animationController, EnemyTransform, Target);
            fleeState = new FleeState(movementAway, animationController, Target);
            deadState = new DeadState(animationController);
            
            stateMachine.AddState(idleState);
            stateMachine.AddState(chaseState);
            stateMachine.AddState(magicAttackState);
            stateMachine.AddState(fleeState);
            stateMachine.AddState(deadState);
            
            stateMachine.AddTransition<IdleState, ChaseState>(() => Target && DistanceToTarget() < data.idleChaseRadius);
            stateMachine.AddTransition<ChaseState, IdleState>(() => Target && DistanceToTarget() > data.chaseIdleRadius);
            stateMachine.AddTransition<ChaseState, MagicAttackState>(() => Target && DistanceToTarget() < data.chaseAttackRadius);
            stateMachine.AddTransition<MagicAttackState, ChaseState>(() => magicAttackState.IsAttackSequenceComplete);
            stateMachine.AddTransition<MagicAttackState, FleeState>(() => Target && DistanceToTarget() < data.attackFleeRadius);
            stateMachine.AddTransition<FleeState, MagicAttackState>(() => Target && DistanceToTarget() > data.fleeAttackRadius);
            
            stateMachine.AddAnyTransition<IdleState>(() => !Target && coreSetup.Health.Health > 0f);
            stateMachine.AddAnyTransition<DeadState>(() => coreSetup.Health.Health <= 0f);
            
            stateMachine.Initialize(idleState);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            coreSetup.Cleanup();
            magicAttackSetup.Cleanup();
            
            UI.Cleanup();
        }
    }
}