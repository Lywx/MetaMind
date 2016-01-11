namespace MetaMind.Session.Operations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Engine.Entities.Controls.Item.Data;

    #region Operation Description

    public partial class OperationDescription : IOperationDescription
    {
        public OperationDescription(
            string name,
            string description,
            string path)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            this.Name        = name;
            this.Description = description;
            this.Path        = path;

            this.Reset();
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Path { get; private set; }

        public void Reset()
        {
            // Structure
            this.Parent = null;
            this.Children = new List<IOperationDescription>();
        }

        public void Update()
        {
            if (this.Operation != null)
            {
                this.Operation.Update();
            }

            if (this.HasChildren)
            {
                foreach (var operation in this.Children.ToArray())
                {
                    operation.Update();
                }
            }
        }
    }

    #endregion

    #region Operation Organization

    public partial class OperationDescription
    {
        public List<IOperationDescription> Children { get; private set; }

        public bool HasChildren
        {
            get { return this.Children != null && this.Children.Count != 0; }
        }

        public IOperationDescription Parent { get; set; }

        public bool HasParent
        {
            get { return this.Parent != null; }
        }
    }

    #endregion

    #region IComparable

    public partial class OperationDescription
    {
        public int CompareTo(IOperationDescription other)
        {
            return string.Compare(this.Name, other.Name, StringComparison.Ordinal);
        }
    }

    #endregion

    #region Operation Operations

    public partial class OperationDescription
    {

        public IOperation Operation { get; set; }


        public bool IsOperationActivated
        {
            get
            {
                return (this.Operation != null && this.Operation.IsActivated) ||
                       (this.HasChildren && this.ChildrenOperationActivated == this.Children.Count);
            }
        }

        public int ChildrenOperationActivated
        {
            get
            {
                return this.Children.Count(item => item.IsOperationActivated);
            }
        }

        public string OperationStatus
        {
            get
            {
                return this.Operation == null
                           ? ""
                           : this.Operation.IsActivated
                                 ? "ACTIVATED"
                                 : "INACTIVE";
            }
        }

        public void Toggle()
        {
            if (this.Operation != null)
            {
                this.Operation.Toggle();
            }
        }
    }

    #endregion

    #region IBlockViewItemData

    public partial class OperationDescription : IMMBlockViewItemData 
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