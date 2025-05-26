using AI.StateMachine;
using Enemies.CoreLogic;
using UnityEngine;

namespace AI
{
    public abstract class BaseState<T> : IState<T> where T : Enemy
    {
        public virtual void OnEnter(T context)
        {
            Debug.Log($"[{GetType()}] OnEnter");
        }

        public virtual void OnUpdate(T context, float deltaTime)
        {
        }

        public virtual void OnExit(T context)
        {
            Debug.Log($"[{GetType()}] OnExit");
        }
    }
}