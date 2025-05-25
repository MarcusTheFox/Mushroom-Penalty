using AI.StateMachine;
using Core.Interfaces;
using Core.Movement;
using Core.UnityHooks;
using Enemies.Components;
using Enemies.Data;
using Enemies.States;
using Enemies.States.AttackStates;
using Enemies.UI;
using UnityEngine;

namespace Enemies.CoreLogic
{
    public class EnemyMelee : Enemy
    {
        public Transform Target { get; private set; }
        
        private EnemyCoreComponentsSetup coreSetup;
        private IStateMachine<EnemyMelee> stateMachine;
        
        private EnemyMovementSetup movementSetup;

        private EnemyDataSO enemyData;

        public EnemyMelee(UnityEventListener UEL,
            AnimationEventListener AEL,
            InteractableObjectEvents IOE,
            EnemyUI UI,
            Transform enemyTransform,
            Animator animator, Transform target) : base(UEL, AEL, IOE, UI, enemyTransform, animator)
        {
            this.Target = target;
        }

        public void Initialize(EnemyDataSO data)
        {
            enemyData = data;
            coreSetup = new EnemyCoreComponentsSetup(IOE, enemyData.health);
            coreSetup.Initialize();

            movementSetup = new EnemyMovementSetup(EnemyTransform, enemyData.chaseSpeed, enemyData.fleeSpeed);
            movementSetup.Initialize();
        
            UI.Initialize(coreSetup.Health);
            
            InitializeStateMachine();
            
            AEL.OnDead += DestroyEnemy;
        
            UEL.OnUpdateEvent.AddListener(UpdateStateMachine);
            UEL.OnDestroyEvent.AddListener(OnDestroy);
        }

        private void InitializeStateMachine()
        {
            stateMachine = new StateMachine<EnemyMelee>(this);
            ConfigureStateMachine();
        }

        private void ConfigureStateMachine()
        {
            IdleState idleState = new IdleState();
            ChaseState chaseState = new ChaseState(movementSetup.EnemyMovementTowards);
            MeleeAttackState meleeAttackState = new MeleeAttackState();
            DeadState deadState = new DeadState();
            
            stateMachine.AddState(idleState);
            stateMachine.AddState(chaseState);
            stateMachine.AddState(meleeAttackState);
            stateMachine.AddState(deadState);
            
            stateMachine.AddTransition<IdleState, ChaseState>(ctx => ctx.Target && 
                DistanceToTarget(ctx.Target) < enemyData.idleChaseRadius);
            
            stateMachine.AddTransition<ChaseState, IdleState>(ctx => ctx.Target && 
                DistanceToTarget(ctx.Target) > enemyData.chaseIdleRadius);
            
            stateMachine.AddTransition<ChaseState, MeleeAttackState>(ctx => ctx.Target && 
                DistanceToTarget(ctx.Target) < enemyData.chaseAttackRadius);
            
            stateMachine.AddTransition<MeleeAttackState, ChaseState>(ctx => ctx.Target && 
                DistanceToTarget(ctx.Target) > enemyData.attackChaseRadius);
            
            stateMachine.Initialize(idleState);
        }

        private void UpdateStateMachine()
        {
            stateMachine?.Update(Time.deltaTime);
        }

        private float DistanceToTarget(Transform target)
        {
            return Vector3.Distance(target.position, EnemyTransform.position);
        }

        private void OnDestroy()
        {
            UEL.OnDestroyEvent.RemoveListener(OnDestroy);
            UEL.OnUpdateEvent.RemoveListener(UpdateStateMachine);
            
            coreSetup.Cleanup();
            
            UI.Cleanup();
        }

        private void DestroyEnemy()
        {
            AEL.OnDead -= DestroyEnemy;
        }
    }
}