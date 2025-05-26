using Core.UnityHooks;
using Enemies.Components;
using Enemies.States;
using Enemies.States.AttackStates;
using Enemies.UI;
using UnityEngine;

namespace Enemies.CoreLogic
{
    public class EnemyMelee : Enemy
    {
        private EnemyMeleeAttackSetup attackSetup;

        private IdleState idleState;
        private ChaseState chaseState;
        private MeleeAttackState meleeAttackState;
        private DeadState deadState;

        public EnemyMelee(UnityEventListener UEL,
            AnimationEventListener AEL,
            InteractableObjectEvents IOE,
            EnemyUI UI,
            Transform enemyTransform,
            Animator animator,
            Transform target) : base(UEL, AEL, IOE, UI, enemyTransform, animator, target)
        {
        }

        protected override void InitializeComponents()
        {
            base.InitializeComponents();
            
            attackSetup = new EnemyMeleeAttackSetup(AEL, 20f, EnemyTransform, enemyData.targetLayer, enemyData.attackChaseRadius, 90f);
            attackSetup.Initialize();
        }

        protected override void ConfigureStateMachine()
        {
            idleState = new IdleState();
            chaseState = new ChaseState(movementSetup.EnemyMovementTowards, animationController);
            meleeAttackState = new MeleeAttackState(attackSetup.MeleeAttack, animationController);
            deadState = new DeadState();
            
            stateMachine.AddState(idleState);
            stateMachine.AddState(chaseState);
            stateMachine.AddState(meleeAttackState);
            stateMachine.AddState(deadState);
            
            stateMachine.AddTransition<IdleState, ChaseState>(_ => Target && 
                DistanceToTarget() < enemyData.idleChaseRadius);
            
            stateMachine.AddTransition<ChaseState, IdleState>(_ => Target && 
                DistanceToTarget() > enemyData.chaseIdleRadius);
            
            stateMachine.AddTransition<ChaseState, MeleeAttackState>(_ => Target && 
                DistanceToTarget() < enemyData.chaseAttackRadius);
            
            stateMachine.AddTransition<MeleeAttackState, ChaseState>(_ => Target && 
                DistanceToTarget() > enemyData.attackChaseRadius);
            
            stateMachine.AddAnyTransition<IdleState>(_ => !Target);
            
            stateMachine.Initialize(idleState);
        }

        private float DistanceToTarget()
        {
            return Vector3.Distance(Target.position, EnemyTransform.position);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            meleeAttackState.Cleanup();
        }
    }
}