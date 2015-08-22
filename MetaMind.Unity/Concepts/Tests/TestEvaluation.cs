namespace MetaMind.Unity.Concepts.Tests
{
    using Engine;
    using System;
    using System.Linq;

    public class TestEvaluation : GameEntity, ITestEvaluation
    {
        private readonly Test test;

        private readonly TestTimer testTimer;

        /// <summary>
        /// Duration when result changed is reflected in ResultChange and ResultChanged. After the timeout, ResultChanged is false.
        /// </summary>
        private readonly TimeSpan resultChangedTimeout = TimeSpan.FromMinutes(3);

        private DateTime resultChangedMoment;

        private bool resultPassed = true;

        private int resultChange;

        public TestEvaluation(Test test, DateTime initial, TimeSpan period, Func<bool> resultSelector = null)
        {
            if (test == null)
            {
                throw new ArgumentNullException(nameof(test));
            }

            this.test = test;
            this.test.Evaluation = this;

            this.testTimer = new TestTimer(initial, period);
            this.testTimer.Fired += (o, a) => this.UpdateResult();

            // Reset to default before assignment
            this.Reset();

            if (resultSelector != null)
            {
                this.ResultSelector = resultSelector;
            }
        }

        ~TestEvaluation()
        {
            this.Dispose();
        }

        public bool ResultPassed
        {
            get { return this.resultPassed; }
            private set
            {
                var failing = this.resultPassed && !value;
                var succeeding = !this.resultPassed && value;

                this.resultPassed = value;

                if (failing)
                {
                    this.Fail(true);
                }

                if (succeeding)
                {
                    this.Succeed(true);
                }
            }
        }

        public bool ResultChanged { get; private set; }

        private Func<bool> ResultSelectorDefault => () => this.ResultChildrenPassed == this.test.Children.Count;

        public Func<bool> ResultSelector { get; set; }

        public string ResultStatus { get; private set; }

        public Func<string> ResultStatusSelector { get; set; }

        private Func<string> ResultStatusSelectorDefault => () => this.ResultSelector.Invoke() ? "PASSED" : "FAILED";

        public int ResultChange
        {
            get
            {
                return this.test.HasChildren
                           ? this.test.Children.Sum(test => test.Evaluation.ResultChange)
                           : this.resultChange;
            }
        }

        public int ResultChildrenPassed
        {
            get
            {
                return this.test.Children.ToArray()
                    .Count(child => child.Evaluation.ResultPassed);
            }
        }

        #region Events

        public event EventHandler<TestEventArgs> Failed;

        public event EventHandler<TestEventArgs> Succeeded;

        private void OnSucceed(bool isCause)
        {
            if (this.Succeeded != null)
            {
                this.Succeeded(this, new TestEventArgs(isCause));
            }
        }

        private void OnFail(bool isCause)
        {
            if (this.Failed != null)
            {
                this.Failed(this, new TestEventArgs(isCause));
            }
        }

        private void OnResultChange(int change)
        {
            this.resultChange = change;
            this.resultChangedMoment = DateTime.Now;
        }

        #endregion Events

        #region Update

        public void Update()
        {
            this.UpdateTimer();
        }

        public void UpdateResult()
        {
            try
            {
                this.ResultPassed = this.ResultSelector      .Invoke();
                this.ResultStatus = this.ResultStatusSelector.Invoke();
            }
            catch (Exception e)
            {
                var console = this.GameInterop.Console;
                console.WriteLine(e.ToString());

                this.ResultPassed = false;
                this.ResultStatus = "ERROR";
            }

            // true only if not timed out
            this.ResultChanged = DateTime.Now - this.resultChangedMoment < this.resultChangedTimeout;

            // reset if not changed
            if (!this.ResultChanged)
            {
                this.ResetResult();
            }

            this.UpdateChildren();
        }

        private void UpdateChildren()
        {
            foreach (var child in this.test.Children.ToArray())
            {
                child.Update();
            }
        }

        private void UpdateTimer()
        {
            this.testTimer.Update();
        }

        #endregion Update

        #region Operations

        public void Succeed(bool isCause)
        {
            if (this.test.HasParent)
            {
                this.EvaluationOf(this.test.Parent).Succeed(false);
                this.EvaluationOf(this.test.Parent).UpdateResult();
            }

            this.OnResultChange(+1);
            this.OnSucceed(isCause);
        }

        public void Fail(bool isCause)
        {
            if (this.test.HasParent)
            {
                this.EvaluationOf(this.test.Parent).Fail(false);
                this.EvaluationOf(this.test.Parent).UpdateResult();
            }

            this.OnResultChange(-1);
            this.OnFail(isCause);
        }

        public void Reset()
        {
            this.ResetResult();

            this.ResultSelector       = this.ResultSelectorDefault;
            this.ResultStatusSelector = this.ResultStatusSelectorDefault;
        }

        private void ResetResult()
        {
            this.resultChange = 0;
            this.resultChangedMoment = DateTime.MinValue;
        }

        #endregion Operations

        #region IDisosable

        public override void Dispose()
        {
            this.Succeeded = null;
            this.Failed    = null;

            if (this.testTimer != null)
            {
                this.testTimer.Dispose();
            }

            base.Dispose();
        }

        #endregion

        #region Helpers

        private ITestEvaluation EvaluationOf(ITest test)
        {
            return test.Evaluation;
        }

        #endregion
    }
}