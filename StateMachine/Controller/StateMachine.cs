namespace AXitUnityTemplate.StateMachine.Controller
{
    using System;
    using Zenject;
    using System.Linq;
    using System.Collections.Generic;
    using AXitUnityTemplate.StateMachine.Signal;
    using AXitUnityTemplate.StateMachine.Interface;

    public abstract class StateMachine : IStateMachine
    {
        private readonly   SignalBus                signalBus;
        protected readonly Dictionary<Type, IState> TypeToState;

        protected StateMachine(
            List<IState> listState,
            SignalBus signalBus
        )
        {
            this.signalBus   = signalBus;
            this.TypeToState = listState.ToDictionary(state => state.GetType(), state => state);
        }

        public IState CurrentState { get; private set; }

        public void TransitionTo<T>() where T : class, IState { this.TransitionTo(typeof(T)); }

        public void TransitionTo<TState, TModel>(TModel model) where TState : class, IState<TModel>
        {
            var stateType = typeof(TState);
            if (!this.TypeToState.TryGetValue(stateType, out var nextState)) return;

            if (nextState is not TState nextStateT) return;
            nextStateT.Model = model;

            this.InternalStateTransition(nextState);
        }

        public virtual void TransitionTo(Type stateType)
        {
            if (!this.TypeToState.TryGetValue(stateType, out var nextState)) return;

            this.InternalStateTransition(nextState);
        }

        private void InternalStateTransition(IState nextState)
        {
            if (this.CurrentState != null)
            {
                this.CurrentState.Exit();
                this.signalBus.Fire(new OnStateExitSignal(this.CurrentState));
            }

            this.CurrentState = nextState;
            this.signalBus.Fire(new OnStateEnterSignal(this.CurrentState));
            nextState.Enter();
        }
    }
}