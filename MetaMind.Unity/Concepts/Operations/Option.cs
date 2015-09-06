namespace MetaMind.Unity.Concepts.Operations
{
    using System;
    using Engine.Guis.Controls.Items.Data;

    #region Option

    public partial class Option<TTransition> : IOption
    {
        private readonly IOperation operation;

        public Option(IOperation operation, string optionName, string optionDescription, TTransition optionTransition)
        {
            if (operation == null)
            {
                throw new ArgumentNullException("operation");
            }

            if (optionName == null)
            {
                throw new ArgumentNullException("optionName");
            }

            if (optionDescription == null)
            {
                throw new ArgumentNullException("optionDescription");
            }

            if (optionTransition == null)
            {
                throw new ArgumentNullException("optionTransition");
            }

            this.operation = operation;

            this.Name        = optionName;
            this.Description = optionDescription;
            this.Transition  = optionTransition;
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public TTransition Transition { get; private set; }
    }

    #endregion

    #region Option Operations

    public partial class Option<TTransition>
    {
        public void Accept()
        {
            ((IOperationOperations<TTransition>)this.operation).Accept(this.Transition);
        }

        public void Unlock()
        {
            this.operation.UnlockTransition();
        }
    }

    #endregion

    #region

    public partial class Option<TTransition> : IBlockViewItemData
    {
        public string BlockStringRaw
        {
            get { return this.Description; }
        }

        public string BlockLabel
        {
            get { return "DescriptionLabel"; }
        }

        public string BlockFrame
        {
            get { return "DescriptionFrame"; }
        } 
    }

    #endregion
}