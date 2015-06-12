namespace MetaMind.Testimony.Scripting
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using Concepts;
    using Microsoft.FSharp.Compiler.Interactive;
    using Microsoft.FSharp.Core;

    public class FsiSession : IInnerUpdatable
    {
        private const string FsiPath = @"C:\Program Files (x86)\Microsoft SDKs\F#\3.1\Framework\v4.0\fsi.exe";

        private readonly string[] fsiArgs = { FsiPath, "--noninteractive" };

        private readonly StringBuilder @out;

        private readonly StringBuilder error;

        private readonly StringReader inStream;

        private readonly StringWriter outStream;

        private readonly StringWriter errorStream;

        private Shell.FsiEvaluationSession fsiSession;

        private Thread threadCurrent;

        private readonly List<Thread> threadQueue = new List<Thread>();

        public FsiSession()
        {
            this.@out = new StringBuilder();
            this.error  = new StringBuilder();

            this.inStream    = new StringReader("");
            this.outStream   = new StringWriter(this.@out);
            this.errorStream = new StringWriter(this.error);

            this.Start(() =>
                {
                    var fsiConfig   = Shell.FsiEvaluationSession.GetDefaultConfiguration();
                    this.fsiSession = Shell.FsiEvaluationSession.Create(
                        fsiConfig,
                        this.fsiArgs,
                        this.inStream,
                        this.outStream,
                        this.errorStream,
                        FSharpOption<bool>.None);
            });
        }

        public StringBuilder Error
        {
            get { return this.error; }
        }

        public StringBuilder Out
        {
            get { return this.@out; }
        }

        #region FsiEvaluationSession

        public void EvalScript(string filePath)
        {
            if (this.fsiSession != null)
            {
                this.Start(() => this.fsiSession.EvalScript(filePath));
            }
            else
            {
                this.Defer(() => this.EvalScript(filePath));
            }
        }

        public void EvalExpression(string code)
        {
            this.Start(() => this.fsiSession.EvalExpression(code));
        }

        public void EvalInteraction(string code)
        {
            this.Start(() => this.fsiSession.EvalInteraction(code));
        }

        #endregion

        #region Thread

        public void Start(Action action)
        {
            if (this.threadCurrent == null)
            {
                this.threadCurrent = new Thread(() => this.Process(action));
                this.threadCurrent.Start();
            }
            else
            {
                this.Defer(action);
            }
        }

        public void Defer(Action action)
        {
            this.threadQueue.Add(new Thread(() => this.Process(action)));
        }

        private void Process(Action action)
        {
            action();

            if (this.threadQueue.Contains(this.threadCurrent))
            {
                this.threadQueue.Remove(this.threadCurrent);
            }

            this.threadCurrent = null;
        }

        public void Update()
        {
            if (this.threadCurrent == null && 
                this.threadQueue.Count != 0)
            {
                this.threadCurrent = this.threadQueue.Last();
                this.threadCurrent.Start();
            }
        }

        #endregion
    }
}