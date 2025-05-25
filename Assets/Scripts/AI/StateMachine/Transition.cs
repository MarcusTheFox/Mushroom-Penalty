using System;

namespace AI.StateMachine
{
    public class Transition<TContext> : ITransition<TContext>
    {
        public Type FromStateType { get; }
        public Type ToStateType { get; }
        public Func<TContext, bool> Condition { get; }
        public Action<TContext> OnTransitionAction { get; }

        public Transition(Type fromState,
            Type toState,
            Func<TContext, bool> condition,
            Action<TContext> onTransitionAction = null)
        {
            FromStateType = fromState;
            ToStateType = toState;
            Condition = condition;
            OnTransitionAction = onTransitionAction;
        }

        public Transition(Type toState,
            Func<TContext, bool> condition,
            Action<TContext> onTransitionAction = null)
        {
            FromStateType = null;
            ToStateType = toState;
            Condition = condition;
            OnTransitionAction = onTransitionAction;
        }
    }
}