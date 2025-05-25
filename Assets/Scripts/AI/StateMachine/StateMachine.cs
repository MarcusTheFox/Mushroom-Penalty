using System;
using System.Collections.Generic;

namespace AI.StateMachine
{
    public class StateMachine<TContext> : IStateMachine<TContext>
    {
        public IState<TContext> CurrentState { get; private set; }
        public TContext Context { get; }

        private readonly Dictionary<Type, IState<TContext>> states = new();
        private readonly Dictionary<Type, List<ITransition<TContext>>> transitions = new();
        private readonly List<ITransition<TContext>> anyTransitions = new();

        public StateMachine(TContext context)
        {
            Context = context;
        }
        
        public void Initialize(IState<TContext> initialState)
        {
            Type stateType = initialState.GetType();
            
            CurrentState = states[stateType];
            CurrentState.OnEnter(Context);
        }

        public void AddState(IState<TContext> state)
        {
            Type stateType = state.GetType();
            if (states.ContainsKey(stateType)) return;
            
            states[stateType] = state;
            transitions[stateType] = new List<ITransition<TContext>>();
        }

        public void AddTransition(ITransition<TContext> transition)
        {
            Type fromStateType = transition.FromStateType;
            if (transitions.TryGetValue(fromStateType, out List<ITransition<TContext>> stateTransitions))
            {
                stateTransitions.Add(transition);
            }
        }

        public void AddTransition<TFromState, TToState>(Func<TContext, bool> condition,
            Action<TContext> onTransitionAction = null)
            where TFromState : IState<TContext>
            where TToState : IState<TContext>
        {
            Type fromStateType = typeof(TFromState);
            Type toStateType = typeof(TToState);
            AddTransition(new Transition<TContext>(fromStateType, toStateType, condition, onTransitionAction));
        }

        public void AddAnyTransition(ITransition<TContext> transition)
        {
            anyTransitions.Add(transition);
        }

        public void AddAnyTransition<TToState>(Func<TContext, bool> condition,
            Action<TContext> onTransitionAction = null)
            where TToState : IState<TContext>
        {
            Type toStateType = typeof(TToState);
            AddAnyTransition(new Transition<TContext>(null, toStateType, condition, onTransitionAction));
        }

        public void Update(float deltaTime)
        {
            if (CurrentState == null) return;

            ITransition<TContext> triggeredTransition = CheckTransition(anyTransitions);
            
            if (triggeredTransition == null)
            {
                if (transitions.TryGetValue(CurrentState.GetType(), 
                        out List<ITransition<TContext>> currentStateTransitions))
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
                CurrentState.OnUpdate(Context, deltaTime);
            }
        }

        private ITransition<TContext> CheckTransition(List<ITransition<TContext>> transitions)
        {
            if (transitions == null) return null;

            foreach (ITransition<TContext> transition in transitions)
            {
                if (transition.FromStateType == null && transition.ToStateType == CurrentState.GetType()) continue;
                
                if (transition.Condition(Context)) return transition;
            }
            
            return null;
        }

        private void PerformTransition(ITransition<TContext> transition)
        {
            if (!states.TryGetValue(transition.ToStateType, out IState<TContext> nextState)) return;
            
            CurrentState.OnExit(Context);
            transition.OnTransitionAction?.Invoke(Context);
            CurrentState = nextState;
            CurrentState.OnEnter(Context);
        }
    }
}