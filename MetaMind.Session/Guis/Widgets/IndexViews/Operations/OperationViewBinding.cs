namespace MetaMind.Session.Guis.Widgets.IndexViews.Operations
{
    using System;
    using System.Collections.Generic;
    using Engine.Entities.Controls.Item;
    using Engine.Entities.Controls.Item.Data;
    using Engine.Entities.Controls.Views.Controllers;
    using Session.Operations;

    public class OperationViewBinding : IMMViewBinding
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

        public dynamic AddData(IMMViewItem item) => null;

        public dynamic RemoveData(IMMViewItem item) => null;

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