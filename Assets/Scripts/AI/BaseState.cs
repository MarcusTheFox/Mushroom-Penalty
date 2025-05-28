using AI.StateMachine;

namespace AI
{
    public abstract class BaseState : IState
    {
        public virtual void OnEnter()
        {
        }

        public virtual void OnUpdate(float deltaTime)
        {
        }

        public virtual void OnExit()
        {
        }
    }
}