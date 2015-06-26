namespace MetaMind.Testimony.Concepts.Operations
{
    using System;
    using System.Collections.Generic;
    using Engine.Guis.Widgets.Items.Data;

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
                throw new ArgumentNullException("name");
            }

            if (description == null)
            {
                throw new ArgumentNullException("description");
            }

            this.Name        = name;
            this.Description = description;
            this.Path        = path;

            this.Children = new List<IOperationDescription>();
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Path { get; private set; }

        public IOperation Operation { get; set; }

        public void Reset()
        {
            
        }

        public void Update()
        {
            this.Operation.Update();
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

    public partial class OperationDescription
    {
        public int CompareTo(IOperationDescription other)
        {
            return string.Compare(this.Name, other.Name, StringComparison.Ordinal);
        }
    }

    #region IBlockViewItemData

    public partial class OperationDescription : IBlockViewItemData 
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