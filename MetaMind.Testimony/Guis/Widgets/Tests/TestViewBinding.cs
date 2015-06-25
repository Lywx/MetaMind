namespace MetaMind.Testimony.Guis.Widgets.Tests
{
    using System;
    using System.Collections.Generic;
    using Concepts.Tests;
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Data;
    using Engine.Guis.Widgets.Views.Logic;

    public class TestViewBinding : IViewBinding
    {
        private readonly TestOrganizer testOrganizer = new TestOrganizer();

        private readonly TestSession testSession;

        private readonly List<ITest> tests;

        private readonly IViewLogic testViewLogic;

        public TestViewBinding(IViewLogic testViewLogic, List<ITest> tests, TestSession testSession)
        {
            if (testViewLogic == null)
            {
                throw new ArgumentNullException("testViewLogic");
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
            this.testViewLogic = testViewLogic;
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
            this.testOrganizer.Organize(this.tests);

            // Avoid thread context switch 
            this.testViewLogic.ResetItems();
        }

        #endregion
    }
}