namespace MetaMind.Testimony.Guis.Widgets
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    using Concepts.Tests;
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Data;
    using Engine.Guis.Widgets.Views.Logic;

    public class TestViewBinding : IViewBinding
    {
        private readonly ObservableCollection<Test> tests;

        private readonly IViewLogic viewLogic;

        public TestViewBinding(IViewLogic viewLogic, ObservableCollection<Test> tests)
        {
            if (viewLogic == null)
            {
                throw new ArgumentNullException("viewLogic");
            }

            this.viewLogic = viewLogic;

            if (tests == null)
            {
                throw new ArgumentNullException("tests");
            }

            this.tests = tests;
            this.tests.CollectionChanged += this.ChildrenCollectionChanged;
        }

        public IReadOnlyList<object> AllData
        {
            get { return this.tests; }
        }

        public dynamic AddData(IViewItem item)
        {
            return null;
        }

        public dynamic RemoveData(IViewItem item)
        {
            return null;
        }

        private void ChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.viewLogic.ResetItems();
        }
    }
}