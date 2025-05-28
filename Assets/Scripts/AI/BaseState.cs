using AI.StateMachine;
using Enemies.CoreLogic;
using UnityEngine;

namespace AI
{
    public abstract class BaseState : IState<Enemy>
    {
        public virtual void OnEnter(Enemy context)
        {
            Debug.Log($"[{GetType()}] OnEnter");
        }

        public virtual void OnUpdate(Enemy context, float deltaTime)
        {
        }

        public virtual void OnExit(Enemy context)
        {
            Debug.Log($"[{GetType()}] OnExit");
        }
    }
}