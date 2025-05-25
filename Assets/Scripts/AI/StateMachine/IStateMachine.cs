using System;

namespace AI.StateMachine
{
    public interface IStateMachine<TContext>
    {
        IState<TContext> CurrentState { get; }
        TContext Context { get; }

        void Initialize(IState<TContext> initialState);
        void AddState(IState<TContext> state);
        void AddTransition(ITransition<TContext> transition);
        void AddTransition<TFromState, TToState>(Func<TContext, bool> condition,
            Action<TContext> onTransitionAction = null) 
            where TFromState : IState<TContext>
            where TToState : IState<TContext>;
        
        void AddAnyTransition(ITransition<TContext> transition);
        void AddAnyTransition<TToState>(Func<TContext, bool> condition,
            Action<TContext> onTransitionAction = null)
            where TToState : IState<TContext>;
        
        void Update(float deltaTime);
    }
}