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
        private readonly ObservableCollection<Test> viewTests;

        private readonly IViewLogic viewLogic;

        public TestViewBinding(IViewLogic viewLogic, ObservableCollection<Test> viewTests)
        {
            if (viewLogic == null)
            {
                throw new ArgumentNullException("viewLogic");
            }

            this.viewLogic = viewLogic;

            if (viewTests == null)
            {
                throw new ArgumentNullException("viewTests");
            }

            this.viewTests = viewTests;
        }

        #region 

        public dynamic AddData(IViewItem item)
        {
            return null;
        }

        public dynamic RemoveData(IViewItem item)
        {
            return null;
        }

        public IReadOnlyList<object> AllData
        {
            get { return this.viewTests; }
        }

        #endregion

        #region Binding

        public void Bind()
        {
            this.viewTests.CollectionChanged += this.ChildrenCollectionChanged;
        }

        public void Unbind()
        {
            this.viewTests.CollectionChanged -= this.ChildrenCollectionChanged;
        }

        #endregion

        #region Events

        private void ChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.viewLogic.ResetItems();
        }

        #endregion
    }
}