namespace MetaMind.Unity.Concepts.Operations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Engine;
    using Extensions;
    using Guis.Screens;
    using Stateless;

    public class Operation : GameEntity
    {
        public static OperationSession Session { get; set; }
    }

    #region Operation

    public partial class Operation<TProcedure, TTransition> : Operation, IOperation, IOperationOperations<TTransition>
    {
        public Operation(
            StateMachine<TProcedure, TTransition> operationMachine,
            IDictionary<TProcedure, string>   procedureDescriptions,
            IDictionary<TProcedure, TimeSpan> procedureSpans,
            IDictionary<TTransition, string> transitionDescriptions)
        {
            if (operationMachine == null)
            {
                throw new ArgumentNullException(nameof(operationMachine));
            }

            if (procedureDescriptions == null)
            {
                throw new ArgumentNullException(nameof(procedureDescriptions));
            }

            if (procedureSpans == null)
            {
                throw new ArgumentNullException(nameof(procedureSpans));
            }

            if (transitionDescriptions == null)
            {
                throw new ArgumentNullException(nameof(transitionDescriptions));
            }

            this.Machine = operationMachine;

            this.ProcedureNames        = typeof(TProcedure).ToDict<TProcedure>();
            this.ProcedureDescriptions = procedureDescriptions;
            this.ProcedureSpans        = procedureSpans;

            this.TransitionNames = typeof(TTransition).ToDict<TTransition>();
            this.TransitionDescriptions = transitionDescriptions;
        }

        protected StateMachine<TProcedure, TTransition> Machine { get; private set; }
    }

    #endregion

    #region Operation Computation

    public partial class Operation<TProcedure, TTransition>
    {
        public IDictionary<TTransition, string> TransitionNames { get; private set; }

        public IDictionary<TTransition, string> TransitionDescriptions { get; private set; }

        public IDictionary<TProcedure, string> ProcedureNames { get; set; }

        public string ProcedureName => this.ProcedureNames[this.Machine.State];

        public IDictionary<TProcedure, string> ProcedureDescriptions { get; private set; }

        public string ProcedureDescription => this.ProcedureDescriptions[this.Machine.State];

        public IDictionary<TProcedure, TimeSpan> ProcedureSpans { get; private set; }

        private TimeSpan ProcedureSpan => this.ProcedureSpans[this.Machine.State];

        private TimeSpan ProcedureElapsed => DateTime.Now - this.procedureTransitionedMoment;

        public void Accept(TTransition transition)
        {
            this.Machine.Fire(transition);

            this.procedureTransitionedMoment = DateTime.Now;
        }

        private List<IOption> RequestOptions()
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

        private void SendOptions()
        {
            if (Session.IsNotificationEnabled)
            {
                var audio = this.GameInterop.Audio;
                audio.PlayCue(this.transitioningCue);
            }

            var screenManager = this.GameInterop.Screen;
            var mainScreen = (MainScreen)screenManager.Screens.First(s => s is MainScreen);

            screenManager.AddScreen(new OptionScreen(this.ProcedureName, this.ProcedureDescription, this.RequestOptions(), mainScreen.CircularLayers));
        }
    }

    #endregion

    #region Operation Update

    public partial class Operation<TProcedure, TTransition>
    {
        private readonly string transitioningCue = "Operation Transition";

        private DateTime procedureTransitionedMoment;

        private bool IsInitialized { get; set; }

        public bool IsActivated { get; private set; }

        public void Update()
        {
            // Pause updating when inactivated
            if (!this.IsActivated || Session.IsLocked)
            {
                return;
            }

            // Record the first moment of updating
            if (!this.IsInitialized)
            {
                this.IsInitialized = true;

                // Make sure the procedureTransitionedMoment is initialized.
                this.procedureTransitionedMoment = DateTime.Now;
            }

            var shouldTransit = this.ProcedureElapsed > this.ProcedureSpan;
            if (shouldTransit && !Session.IsLocked)
            {
                this.LockTransition();
                this.SendOptions();
            }
        }

        #region Operations

        public void Toggle()
        {
            this.IsActivated = !this.IsActivated;
        }

        public void UnlockTransition()
        {
            Session.Unlock();
        }

        public void LockTransition()
        {
            Session.Lock();
        }

        #endregion
    }

    #endregion
}