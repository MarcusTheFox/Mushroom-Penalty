using Core.Interfaces;
using Core.Movement;
using Core.UnityHooks;
using Enemies.Components;
using Enemies.Data;
using Enemies.Handlers;
using Enemies.States;
using Enemies.States.AttackStates;
using Enemies.UI;
using UnityEngine;

namespace Enemies.CoreLogic
{
    public class EnemyMelee : Enemy, IConfigurable<EnemyMeleeDataSO>
    {
        protected EnemyCoreComponentsSetup coreSetup;
        protected EnemyAnimationController animationController;
        private EnemyMeleeAttackSetup attackSetup;

        private IdleState idleState;
        private ChaseState chaseState;
        private MeleeAttackState meleeAttackState;
        private DeadState deadState;
        private IConfigurable<EnemyMeleeDataSO> configurableImplementation;
        private EnemyMeleeDataSO data;
        private EnemyMovementTowards movementTowards;

        public EnemyMelee(UnityEventListener UEL,
            AnimationEventListener AEL,
            InteractableObjectEvents IOE,
            EnemyUI UI,
            Transform enemyTransform,
            Animator animator,
            Transform target) : base(UEL, AEL, IOE, UI, enemyTransform, animator, target)
        {
        }

        public void Configure(EnemyMeleeDataSO data)
        {
            this.data = data;
        }

        protected override void InitializeComponents()
        {
            animationController = new EnemyAnimationController(animator);
            
            coreSetup = new EnemyCoreComponentsSetup(IOE, data.health);
            coreSetup.Initialize();

            attackSetup = new EnemyMeleeAttackSetup(AEL, EnemyTransform, data);
            attackSetup.Initialize();
            
            movementTowards = new EnemyMovementTowards(EnemyTransform, data.speed);
            
            UI.Initialize(coreSetup.Health);
        }

        protected override void ConfigureStateMachine()
        {
            idleState = new IdleState();
            chaseState = new ChaseState(movementTowards, animationController, Target);
            meleeAttackState = new MeleeAttackState(attackSetup.MeleeAttack, animationController);
            deadState = new DeadState(animationController);
            
            stateMachine.AddState(idleState);
            stateMachine.AddState(chaseState);
            stateMachine.AddState(meleeAttackState);
            stateMachine.AddState(deadState);
            
            stateMachine.AddTransition<IdleState, ChaseState>(_ => Target && DistanceToTarget() < data.idleChaseRadius);
            stateMachine.AddTransition<ChaseState, IdleState>(_ => Target && DistanceToTarget() > data.chaseIdleRadius);
            stateMachine.AddTransition<ChaseState, MeleeAttackState>(_ => Target && DistanceToTarget() < data.chaseAttackRadius);
            stateMachine.AddTransition<MeleeAttackState, ChaseState>(_ => Target && DistanceToTarget() > data.attackChaseRadius);
            
            stateMachine.AddAnyTransition<IdleState>(_ => !Target && coreSetup.Health.Health > 0f);
            stateMachine.AddAnyTransition<DeadState>(_ => coreSetup.Health.Health <= 0f);
            
            stateMachine.Initialize(idleState);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            coreSetup.Cleanup();
            meleeAttackState.Cleanup();
            
            UI.Cleanup();
        }
    }
}