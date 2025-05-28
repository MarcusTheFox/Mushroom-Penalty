using AI.StateMachine;
using Enemies.CoreLogic;

namespace AI
{
    public abstract class BaseState : IState<Enemy>
    {
        public virtual void OnEnter(Enemy context)
        {
        }

        public virtual void OnUpdate(Enemy context, float deltaTime)
        {
        }

        public virtual void OnExit(Enemy context)
        {
        }
    }
}