namespace MetaMind.Testimony.Concepts.Operations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Engine;
    using Guis.Screens;
    using Stateless;

    #region Operation

    public partial class Operation<TProcedure, TTransition> : IOperation, IOperationOperations<TTransition>
    {
        public Operation(
            StateMachine<TProcedure, TTransition> operationMachine,
            IDictionary<TProcedure, string>   procedureNames,
            IDictionary<TProcedure, string>   procedureDescriptions,
            IDictionary<TProcedure, TimeSpan> procedureSpans,
            IDictionary<TTransition, string> transitionNames,
            IDictionary<TTransition, string> transitionDescriptions)
        {
            if (operationMachine == null)
            {
                throw new ArgumentNullException("operationMachine");
            }

            if (procedureNames == null)
            {
                throw new ArgumentNullException("procedureNames");
            }

            if (procedureDescriptions == null)
            {
                throw new ArgumentNullException("procedureDescriptions");
            }

            if (procedureSpans == null)
            {
                throw new ArgumentNullException("procedureSpans");
            }

            if (transitionNames == null)
            {
                throw new ArgumentNullException("transitionNames");
            }

            if (transitionDescriptions == null)
            {
                throw new ArgumentNullException("transitionDescriptions");
            }

            this.Machine = operationMachine;

            this.ProcedureNames        = procedureNames;
            this.ProcedureDescriptions = procedureDescriptions;
            this.ProcedureSpans        = procedureSpans;

            this.TransitionNames        = transitionNames;
            this.TransitionDescriptions = transitionDescriptions;
        }
        
        protected StateMachine<TProcedure, TTransition> Machine { get; private set; }
    }

    #endregion

    #region Operation Computation

    public partial class Operation<TProcedure, TTransition> : GameEntity
    {
        /// <remarks>
        /// Compatible with F# dict.
        /// </remarks>
        public IDictionary<TProcedure, string> ProcedureNames { get; private set; }

        public IDictionary<TProcedure, string> ProcedureDescriptions { get; private set; }

        public IDictionary<TProcedure, TimeSpan> ProcedureSpans { get; private set; }

        public IDictionary<TTransition, string> TransitionNames { get; private set; }

        public IDictionary<TTransition, string> TransitionDescriptions { get; private set; }

        public void Accept(TTransition trigger)
        {
            this.Machine.Fire(trigger);

            this.procedureTransitionedMoment = DateTime.Now;
        }

        public List<IOption> Request()
        {
            var transitions = this.Machine.PermittedTriggers;

            return (from transition in transitions
                    let transitionName        = this.TransitionNames[transition]
                    let transitionDescription = this.TransitionDescriptions[transition]
                    select
                        new Option<TTransition>(
                        this,
                        transitionName,
                        transitionDescription,
                        transition)).Cast<IOption>().ToList();
        }

        public void Send()
        {
            var screenManager = this.GameInterop.Screen;
            screenManager.AddScreen(new OptionScreen(this.Request()));
        }
    }

    #endregion

    public partial class Operation<TProcedure, TTransition>
    {
        private DateTime procedureTransitionedMoment;

        public bool IsProcedureTransitioning { get; set; }

        public void Start()
        {
        }

        public void End()
        {
        }

        public void Update()
        {
            var isProcedureShouldTransit = DateTime.Now - this.procedureTransitionedMoment
                                           > this.ProcedureSpans[this.Machine.State];
            if (isProcedureShouldTransit && !this.IsProcedureTransitioning)
            {
                this.Send();
            }
        }
    }
}