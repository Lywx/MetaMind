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
            string                                operationName,
            string                                operationDescription,
            StateMachine<TProcedure, TTransition> operationMachine,
            Dictionary<TProcedure, string>   procedureNames,
            Dictionary<TProcedure, string>   procedureDescriptions,
            Dictionary<TProcedure, TimeSpan> procedureSpans,
            Dictionary<TTransition, string> transitionNames,
            Dictionary<TTransition, string> transitionDescriptions)
            : this(operationName, operationDescription, operationMachine)
        {
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

            this.ProcedureNames        = procedureNames;
            this.ProcedureDescriptions = procedureDescriptions;
            this.ProcedureSpans        = procedureSpans;

            this.TransitionNames        = transitionNames;
            this.TransitionDescriptions = transitionDescriptions;
        }
        
        private Operation(string operationName, string operationDescription, StateMachine<TProcedure, TTransition> operationMachine)
        {
            if (operationName == null)
            {
                throw new ArgumentNullException("operationName");
            }

            if (operationDescription == null)
            {
                throw new ArgumentNullException("operationDescription");
            }

            if (operationMachine == null)
            {
                throw new ArgumentNullException("operationMachine");
            }

            this.Name        = operationName;
            this.Description = operationDescription;
            this.Machine     = operationMachine;
        }

        public string Name { get; private set;}

        public string Description { get; private set; }

        protected StateMachine<TProcedure, TTransition> Machine { get; private set; }
    }

    #endregion

    #region Operation Computation

    public partial class Operation<TProcedure, TTransition> : GameEntity
    {
        public Dictionary<TProcedure, string> ProcedureNames { get; private set; }

        public Dictionary<TProcedure, string> ProcedureDescriptions { get; private set; }

        public Dictionary<TProcedure, TimeSpan> ProcedureSpans { get; private set; }

        public Dictionary<TTransition, string> TransitionNames { get; private set; }

        public Dictionary<TTransition, string> TransitionDescriptions { get; private set; }

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