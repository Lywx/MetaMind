namespace MetaMind.Testimony.Guis.Widgets.IndexViews.Operations
{
    using System;
    using System.Collections.Generic;
    using Concepts.Operations;
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Data;
    using Engine.Guis.Widgets.Views.Logic;

    public class OperationViewBinding : IViewBinding
    {
        private readonly IViewLogic operationViewLogic;

        private readonly IOperationDescription operation;

        private readonly OperationSession operationSession;

        private OperationOrganizer operationOrganizer;

        public OperationViewBinding(
            IViewLogic            operationViewLogic,
            IOperationDescription operation,
            OperationSession      operationSession)
        {
            if (operationViewLogic == null)
            {
                throw new ArgumentNullException("operationViewLogic");
            }

            if (operation == null)
            {
                throw new ArgumentNullException("operation");
            }

            if (operationSession == null)
            {
                throw new ArgumentNullException("operationSession");
            }

            this.operationViewLogic = operationViewLogic;

            this.operation = operation;
            this.operationSession = operationSession;

            this.operationOrganizer = new OperationOrganizer();
        }

        public dynamic AddData(IViewItem item)
        {
            return null;
        }

        public dynamic RemoveData(IViewItem item)
        {
            return null;
        }

        public IReadOnlyList<object> AllData { get; private set; }

        #region Binding

        public void Bind()
        {
            this.operationSession.FsiSession.ThreadStopped += this.FsiSessionThreadStopped;
        }

        public void Unbind()
        {
            this.operationSession.FsiSession.ThreadStopped -= this.FsiSessionThreadStopped;
        }

        #endregion

        #region Events

        private void FsiSessionThreadStopped(object sender, EventArgs e)
        {
            this.operationOrganizer.Organize(this.operation);

            // Avoid thread context switch 
            this.operationViewLogic.ResetItems();
        }

        #endregion
    }
}