using System;

namespace AI.StateMachine
{
    public interface IStateMachine
    {
        IState CurrentState { get; }

        void Initialize(IState initialState);
        void AddState(IState state);
        void AddTransition(ITransition transition);
        void AddTransition<TFromState, TToState>(Func<bool> condition,
            Action onTransitionAction = null) 
            where TFromState : IState
            where TToState : IState;
        
        void AddAnyTransition(ITransition transition);
        void AddAnyTransition<TToState>(Func<bool> condition,
            Action onTransitionAction = null)
            where TToState : IState;
        
        void Update(float deltaTime);
    }
}