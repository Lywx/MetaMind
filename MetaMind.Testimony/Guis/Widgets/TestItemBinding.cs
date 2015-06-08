namespace MetaMind.Testimony.Guis.Widgets
{
    using System;
    using System.Collections.Generic;

    using Concepts.Tests;
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Data;

    public class TestItemBinding : IViewItemBinding
    {
        private readonly ITest root;

        public TestItemBinding(ITest root)
        {
            if (root == null)
            {
                throw new ArgumentNullException("root");
            }

            this.root = root;
        }

        public dynamic AddData(IViewItem item)
        {
            return null;
        }

        public dynamic RemoveData(IViewItem item)
        {
            return null;
        }

        public IList<dynamic> AllData()
        {
            return (IList<dynamic>)root.Children;
        }
    }
}