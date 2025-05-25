using System;

namespace AI.StateMachine
{
    public interface ITransition<TContext>
    {
        Type FromStateType { get; }
        Type ToStateType { get; }
        Func<TContext, bool> Condition { get; }
        Action<TContext> OnTransitionAction { get; }
    }
}