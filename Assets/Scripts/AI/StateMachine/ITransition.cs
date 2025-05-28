using System;

namespace AI.StateMachine
{
    public interface ITransition
    {
        Type FromStateType { get; }
        Type ToStateType { get; }
        Func<bool> Condition { get; }
        Action OnTransitionAction { get; }
    }
}