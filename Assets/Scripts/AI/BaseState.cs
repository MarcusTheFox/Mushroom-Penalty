using AI.StateMachine;
using Core.Interfaces;
using Enemies.CoreLogic;
using UnityEngine;

namespace AI
{
    public abstract class BaseState : IState<EnemyMelee>
    {
        public virtual void OnEnter(EnemyMelee context)
        {
            Debug.Log($"[{GetType()}] OnEnter");
        }

        public virtual void OnUpdate(EnemyMelee context, float deltaTime)
        {
        }

        public virtual void OnExit(EnemyMelee context)
        {
            Debug.Log($"[{GetType()}] OnExit");
        }
    }
}