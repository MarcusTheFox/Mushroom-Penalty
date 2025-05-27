using AI.StateMachine;
using Core.UnityHooks;
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

        public void Initialize()
        {
            InitializeComponents();
            InitializeStateMachine();
            ConfigureEvents();
        }

        protected virtual void InitializeComponents()
        {
        }

        private void InitializeStateMachine()
        {
            stateMachine = new StateMachine<Enemy>(this);
            ConfigureStateMachine();
        }

        private void ConfigureEvents()
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
        }

        protected float DistanceToTarget()
        {
            return Vector3.Distance(Target.position, EnemyTransform.position);
        }
    }
}