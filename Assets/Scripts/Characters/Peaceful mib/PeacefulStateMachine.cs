using UnityEngine;
using System.Collections.Generic;

public class PeacefulStateMachine
{
    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();
    public IState CurrentState { get; private set; }

    public void AddState(StateType type, IState state)
    {
        states[type] = state;
    }

    public void ChangeState(StateType type)
    {
        CurrentState?.Exit();
        CurrentState = states[type];
        CurrentState?.Enter();
    }
}


