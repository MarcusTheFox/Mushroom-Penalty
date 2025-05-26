using AI.StateMachine;
using Core.UnityHooks;
using Enemies.Components;
using Enemies.Data;
using Enemies.Handlers;
using Enemies.UI;
using UnityEngine;

namespace Enemies.CoreLogic
{
    public abstract class Enemy
    {
        public Transform Target { get; private set; }
        protected UnityEventListener UEL { get; }
        protected AnimationEventListener AEL { get; }
        protected InteractableObjectEvents IOE  { get; }
        protected EnemyUI UI { get; }
        protected Transform EnemyTransform { get; }
        protected Animator animator { get; }
        
        protected EnemyDataSO enemyData;
        
        protected EnemyCoreComponentsSetup coreSetup;
        protected EnemyMovementSetup movementSetup;
        protected EnemyAnimationController animationController;
        
        protected IStateMachine<Enemy> stateMachine;

        protected Enemy(UnityEventListener UEL,
            AnimationEventListener AEL,
            InteractableObjectEvents IOE,
            EnemyUI UI,
            Transform enemyTransform,
            Animator animator,
            Transform target)
        {
            this.UEL = UEL;
            this.AEL = AEL;
            this.IOE = IOE;
            this.UI = UI;
            EnemyTransform = enemyTransform;
            this.animator = animator;
            Target = target;
        }

        public void Initialize(EnemyDataSO data)
        {
            enemyData = data;
            
            InitializeComponents();
            InitializeStateMachine();
            ConfigureEvents();
        }

        protected virtual void InitializeComponents()
        {
            animationController = new EnemyAnimationController(animator);
            
            coreSetup = new EnemyCoreComponentsSetup(IOE, enemyData.health);
            coreSetup.Initialize();

            movementSetup = new EnemyMovementSetup(EnemyTransform, enemyData.chaseSpeed, enemyData.fleeSpeed);
            movementSetup.Initialize();
            
            UI.Initialize(coreSetup.Health);
        }

        private void InitializeStateMachine()
        {
            stateMachine = new StateMachine<Enemy>(this);
            ConfigureStateMachine();
        }

        protected void ConfigureEvents()
        {
            UEL.OnUpdateEvent.AddListener(UpdateStateMachine);
            UEL.OnDestroyEvent.AddListener(OnDestroy);
        }

        protected abstract void ConfigureStateMachine();

        private void UpdateStateMachine()
        {
            stateMachine?.Update(Time.deltaTime);
        }

        protected virtual void OnDestroy()
        {
            UEL.OnDestroyEvent.RemoveListener(OnDestroy);
            UEL.OnUpdateEvent.RemoveListener(UpdateStateMachine);
            
            coreSetup.Cleanup();
            
            UI.Cleanup();
        }
    }
}