using AI.StateMachine;
using Core.UnityHooks;
using Enemies.CoreLogic.Context;
using UnityEngine;

namespace Enemies.CoreLogic
{
    public abstract class Enemy
    {
        private readonly Transform Target;
        private readonly UnityEventListener UEL;
        private readonly Transform EnemyTransform;

        protected IStateMachine stateMachine;

        protected Enemy(EnemyContext context)
        {
            UEL = context.UEL;
            EnemyTransform = context.EnemyTransform;
            Target = context.Target;
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
            stateMachine = new StateMachine();
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