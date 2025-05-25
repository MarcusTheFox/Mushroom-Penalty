namespace AI.StateMachine
{
    public interface IState<TContext>
    {
        void OnEnter(TContext context);
        void OnUpdate(TContext context, float deltaTime);
        void OnExit(TContext context);
    }
}