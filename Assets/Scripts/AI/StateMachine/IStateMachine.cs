namespace AI.StateMachine
{
    public interface IStateMachine<TContext>
    {
        IState<TContext> CurrentState { get; }
        TContext Context { get; }

        void Initialize(IState<TContext> initialState);
        void AddState(IState<TContext> state);
        void AddTransition(ITransition<TContext> transition);
        void AddAnyTransition(ITransition<TContext> transition);

        void Update(float deltaTime);
    }
}