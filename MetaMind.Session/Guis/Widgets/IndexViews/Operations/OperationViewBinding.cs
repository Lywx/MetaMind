namespace MetaMind.Session.Guis.Widgets.IndexViews.Operations
{
    using System;
    using System.Collections.Generic;
    using Concepts.Operations;
    using Engine.Gui.Controls.Item;
    using Engine.Gui.Controls.Item.Data;
    using Engine.Gui.Controls.Views.Logic;

    public class OperationViewBinding : IViewBinding
    {
        private readonly IMMViewController operationViewController;

        private readonly IOperationDescription operation;

        private readonly OperationSession operationSession;

        private readonly OperationOrganizer operationOrganizer = new OperationOrganizer();

        public OperationViewBinding(
            IMMViewController            operationViewController,
            IOperationDescription operation,
            OperationSession      operationSession)
        {
            if (operationViewController == null)
            {
                throw new ArgumentNullException(nameof(operationViewController));
            }

            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation));
            }

            if (operationSession == null)
            {
                throw new ArgumentNullException(nameof(operationSession));
            }

            this.operationViewController = operationViewController;
            this.operation          = operation;
            this.operationSession   = operationSession;
        }

        #region Binding

        public dynamic AddData(IViewItem item) => null;

        public dynamic RemoveData(IViewItem item) => null;

        public IReadOnlyList<object> AllData => this.operation.Children;

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
            this.operationViewController.ResetItems();
        }

        #endregion
    }
}