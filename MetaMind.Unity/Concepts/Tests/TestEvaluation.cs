namespace MetaMind.Unity.Concepts.Tests
{
    using Engine;
    using System;
    using System.Linq;
    using Events;

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

        public TestEvaluation(Test test, DateTime initial, TimeSpan period) 
        {
            if (test == null)
            {
                throw new ArgumentNullException(nameof(test));
            }

            this.test = test;
            this.test.Evaluation = this;

            this.testTimer = new TestTimer(initial, period);
            this.testTimer.Fired += (o, a) => this.UpdateResult();

            this.Reset();
        }

        public TestEvaluation(Test test, DateTime initial, TimeSpan period, Func<bool> resultSelector) 
            : this(test, initial, period)
        {
            if (resultSelector == null)
            {
                throw new ArgumentNullException(nameof(resultSelector));
            }

            this.ResultSelector = resultSelector;
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

        public int ResultAllPassed
        {
            get
            {
                return this.test.AllCollection.ToArray()
                    .Count(child => child.Evaluation.ResultPassed);
            }
        }

        public float ResultAllPassedRate => ((float)this.ResultAllPassed / this.test.AllCollection.Count() * 100); 

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

        public event EventHandler<TestEvaluationEventArgs> Failed = delegate {};

        public event EventHandler<TestEvaluationEventArgs> Succeeded = delegate { };

        #endregion

        #region Event On

        private void OnSucceed(bool isSource)
        {
            this.Succeeded.Invoke(this, new TestEvaluationEventArgs(isSource));
        }

        private void OnFail(bool isSource)
        {
            this.Failed(this, new TestEvaluationEventArgs(isSource));
        }

        private void OnResultChange(int change)
        {
            this.resultChange = change;
            this.resultChangedMoment = DateTime.Now;
        }

        #endregion 

        // TODO(Critical): Not implemented test action.

        public Action<TestEvaluationEventArgs> FailedAction = delegate { };

        public Action<TestEvaluationEventArgs> ScceddedAction = delegate { };

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
                var console = this.Interop.Console;
                console.WriteLine(e.ToString(), "ERROR");

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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Succeeded = null;
                this.Failed    = null;

                this.testTimer?.Dispose();
            }

            base.Dispose(disposing);
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