using System;
using System.Collections.Generic;

namespace AI.StateMachine
{
    public class StateMachine : IStateMachine
    {
        public IState CurrentState { get; private set; }

        private readonly Dictionary<Type, IState> states = new();
        private readonly Dictionary<Type, List<ITransition>> transitions = new();
        private readonly List<ITransition> anyTransitions = new();
        
        public void Initialize(IState initialState)
        {
            Type stateType = initialState.GetType();
            
            CurrentState = states[stateType];
            CurrentState.OnEnter();
        }

        public void AddState(IState state)
        {
            Type stateType = state.GetType();
            if (states.ContainsKey(stateType)) return;
            
            states[stateType] = state;
            transitions[stateType] = new List<ITransition>();
        }

        public void AddTransition(ITransition transition)
        {
            Type fromStateType = transition.FromStateType;
            if (transitions.TryGetValue(fromStateType, out List<ITransition> stateTransitions))
            {
                stateTransitions.Add(transition);
            }
        }

        public void AddTransition<TFromState, TToState>(Func<bool> condition,
            Action onTransitionAction = null)
            where TFromState : IState
            where TToState : IState
        {
            Type fromStateType = typeof(TFromState);
            Type toStateType = typeof(TToState);
            AddTransition(new Transition(fromStateType, toStateType, condition, onTransitionAction));
        }

        public void AddAnyTransition(ITransition transition)
        {
            anyTransitions.Add(transition);
        }

        public void AddAnyTransition<TToState>(Func<bool> condition,
            Action onTransitionAction = null)
            where TToState : IState
        {
            Type toStateType = typeof(TToState);
            AddAnyTransition(new Transition(null, toStateType, condition, onTransitionAction));
        }

        public void Update(float deltaTime)
        {
            if (CurrentState == null) return;

            ITransition triggeredTransition = CheckTransition(anyTransitions);
            
            if (triggeredTransition == null)
            {
                if (transitions.TryGetValue(CurrentState.GetType(), 
                        out List<ITransition> currentStateTransitions))
                {
                    triggeredTransition = CheckTransition(currentStateTransitions);
                }
            }

            if (triggeredTransition != null)
            {
                PerformTransition(triggeredTransition);
            }
            else
            {
                CurrentState.OnUpdate(deltaTime);
            }
        }

        private ITransition CheckTransition(List<ITransition> transitions)
        {
            if (transitions == null) return null;

            foreach (ITransition transition in transitions)
            {
                if (transition.FromStateType == null && transition.ToStateType == CurrentState.GetType()) continue;
                
                if (transition.Condition()) return transition;
            }
            
            return null;
        }

        private void PerformTransition(ITransition transition)
        {
            if (!states.TryGetValue(transition.ToStateType, out IState nextState)) return;
            
            CurrentState.OnExit();
            transition.OnTransitionAction?.Invoke();
            CurrentState = nextState;
            CurrentState.OnEnter();
        }
    }
}