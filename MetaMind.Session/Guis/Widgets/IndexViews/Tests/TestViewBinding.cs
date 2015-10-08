namespace MetaMind.Session.Guis.Widgets.IndexViews.Tests
{
    using System;
    using System.Collections.Generic;
    using Concepts.Tests;
    using Engine.Gui.Controls.Item;
    using Engine.Gui.Controls.Item.Data;
    using Engine.Gui.Controls.Views.Logic;

    public class TestViewBinding : IViewBinding
    {
        private readonly TestOrganizer treeOrganizer = new TestOrganizer();

        private readonly TestSession testSession;

        private readonly List<ITest> tests;

        private readonly IMMViewController testViewController;

        public TestViewBinding(IMMViewController testViewController, List<ITest> tests, TestSession testSession)
        {
            if (testViewController == null)
            {
                throw new ArgumentNullException("testViewController");
            }

            if (tests == null)
            {
                throw new ArgumentNullException("tests");
            }

            if (testSession == null)
            {
                throw new ArgumentNullException("testSession");
            }

            this.tests         = tests;
            this.testViewController = testViewController;
            this.testSession   = testSession;
        }

        #region 

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

        #endregion

        #region Binding

        public void Bind()
        {
            this.testSession.FsiSession.ThreadStopped += this.FsiSessionThreadStopped;
        }

        public void Unbind()
        {
            this.testSession.FsiSession.ThreadStopped -= this.FsiSessionThreadStopped;
        }

        #endregion

        #region Events

        private void FsiSessionThreadStopped(object sender, EventArgs e)
        {
            this.treeOrganizer.Organize(this.tests);

            // Avoid thread context switch 
            this.testViewController.ResetItems();
        }

        #endregion
    }
}