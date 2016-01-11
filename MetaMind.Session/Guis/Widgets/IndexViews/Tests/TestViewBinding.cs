namespace MetaMind.Session.Guis.Widgets.IndexViews.Tests
{
    using System;
    using System.Collections.Generic;
    using Engine.Entities.Controls.Item;
    using Engine.Entities.Controls.Item.Data;
    using Engine.Entities.Controls.Views.Controllers;
    using Session.Tests;

    public class TestViewBinding : IMMViewBinding
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

        public dynamic AddData(IMMViewItem item)
        {
            return null;
        }

        public dynamic RemoveData(IMMViewItem item)
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