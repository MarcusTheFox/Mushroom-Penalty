using System;

namespace AI.StateMachine
{
    public class Transition : ITransition
    {
        public Type FromStateType { get; }
        public Type ToStateType { get; }
        public Func<bool> Condition { get; }
        public Action OnTransitionAction { get; }

        public Transition(Type fromState,
            Type toState,
            Func<bool> condition,
            Action onTransitionAction = null)
        {
            FromStateType = fromState;
            ToStateType = toState;
            Condition = condition;
            OnTransitionAction = onTransitionAction;
        }

        public Transition(Type toState,
            Func<bool> condition,
            Action onTransitionAction = null)
        {
            FromStateType = null;
            ToStateType = toState;
            Condition = condition;
            OnTransitionAction = onTransitionAction;
        }
    }
}