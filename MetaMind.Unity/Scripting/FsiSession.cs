namespace MetaMind.Unity.Scripting
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using Engine;
    using Microsoft.FSharp.Compiler.Interactive;
    using Microsoft.FSharp.Core;
    using FsiEvaluationSession = Microsoft.FSharp.Compiler.Interactive.Shell.FsiEvaluationSession;

    public class FsiSession : GameEntity, IInnerUpdatable
    {
        #region Fsi

        private readonly string fsiPath =
            @"C:\Program Files (x86)\Microsoft SDKs\F#\4.0\Framework\v4.0\FsiAnyCPU.exe";

        private Shell.FsiEvaluationSessionHostConfig fsiConfig;

        private string[] fsiArgv => new string[]{ this.fsiPath, "--nologo", "--gui-" };

        private readonly StringBuilder outBuilder;

        private readonly StringBuilder errorBuilder;

        private readonly StringReader inReader;

        private readonly StringWriter outWriter;

        private readonly StringWriter errorWriter;

        private FsiEvaluationSession fsiSession;

        public bool Verbose { get; set; }

        public StringBuilder Error => this.errorBuilder;

        public StringBuilder Out => this.outBuilder;

        #endregion

        #region Thread

        private Thread threadCurrent;

        private readonly List<Thread> threadsQueued = new List<Thread>();

        #endregion

        public FsiSession()
        {
            this.outBuilder = new StringBuilder();
            this.errorBuilder = new StringBuilder();

            this.inReader    = new StringReader("");
            this.outWriter   = new StringWriter(this.outBuilder);
            this.errorWriter = new StringWriter(this.errorBuilder);

            this.StartThread("FsiSession.CreateSession", () =>
            {
                this.fsiConfig  = FsiEvaluationSession.GetDefaultConfiguration();
                this.fsiSession = FsiEvaluationSession.Create(
                    fsiConfig  : this.fsiConfig,
                    argv       : this.fsiArgv,
                    inReader   : this.inReader,
                    outWriter  : this.outWriter,
                    errorWriter: this.errorWriter,
                    collectible: new FSharpOption<bool>(true));
            });
        }

        #region Events

        /// <summary>
        /// End of a series of asynchronous session
        /// </summary>>
        public event EventHandler ThreadStopped = delegate {};

        /// <summary>
        /// Start of a series of asynchronous session
        /// </summary>>
        public event EventHandler ThreadStarted = delegate {};

        private void OnThreadStopped()
        {
            // Safe threading
            var stopped = this.ThreadStopped;

            stopped?.Invoke(this, EventArgs.Empty);
        }

        private void OnThreadStarted()
        {
            // Safe threading
            var started = this.ThreadStarted;

            started?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Fsi Evaluation

        private void Clear()
        {
            this.Out  .Clear();
            this.Error.Clear();
        }

        public void EvalScript(string filePath)
        {
            var actionName = "FsiSession.EvalScript";

            if (this.fsiSession != null)
            {
                this.StartThread(actionName, () => this.DoEvalScript(filePath));
            }
            else
            {
                this.DeferThread(actionName, () => this.EvalScript(filePath));
            }
        }

        ///<summary>
        /// It is used to debug interactively with FsiDebugger. 
        ///</summary> 
        public object EvalExpression(string code)
        {
            var expression = this.fsiSession.EvalExpression(code);

            this.WriteToOutput();

            return expression.Value.ReflectionValue;
        }

        ///<summary>
        /// It is used to debug interactively with FsiDebugger. 
        ///</summary> 
        public void EvalInteraction(string code)
        {
            this.fsiSession.EvalInteraction(code);

            this.WriteToOutput();
        }

        private void DoEvalScript(string filePath)
        {
            try
            {
                this.fsiSession.EvalScript(filePath);

                this.WriteToOutput();
            }
            catch (Exception)
            {
                var console = this.GameInterop.Console;

                console.WriteLine(this.Out  .ToString(), "DEBUG");
                console.WriteLine(this.Error.ToString(), "ERROR");

                this.Clear();
            }
        }

        private void WriteToOutput()
        {
            if (this.Verbose)
            {
                this.WriteToConsole();
            }
            else
            {
#if DEBUG
                this.WriteToDebug();
#endif
            }
        }

        private void WriteToDebug()
        {
            Debug.WriteLine(this.Out.ToString());
            Debug.WriteLine(this.Error.ToString());

            this.Clear();
        }

        private void WriteToConsole()
        {
            var console = this.GameInterop.Console;

            console.WriteLine(this.Out  .ToString(), "DEBUG");
            console.WriteLine(this.Error.ToString(), "ERROR");

            this.Clear();
        }

        #endregion

        #region Thread

        protected void ContinueThread()
        {
            if (this.threadCurrent == null &&
                this.threadsQueued.Count != 0)
            {
                this.threadCurrent = this.threadsQueued.First();
                this.threadCurrent.Start();
            }
        }

        protected void DeferThread(string actionName, Action action)
        {
            this.threadsQueued.Add(new Thread(() => this.ProcessThread(action)) { Name = actionName });
        }

        protected void StartThread(string actionName, Action action)
        {
            if (this.threadCurrent == null)
            {
                this.threadCurrent = new Thread(() => this.ProcessThread(action)) { Name = actionName };
                this.threadCurrent.Start();
            }
            else
            {
                this.DeferThread(actionName, action);
            }
        }

        protected void ProcessThread(Action action)
        {
            if (this.threadsQueued.Count == 0)
            {
                this.OnThreadStarted();
            }

            action();

            if (this.threadsQueued.Contains(this.threadCurrent))
            {
                this.threadsQueued.Remove(this.threadCurrent);
            }

            this.threadCurrent = null;

            if (this.threadsQueued.Count == 0)
            {
                this.OnThreadStopped();
            }
        }

        #endregion

        #region Update

        public void Update()
        {
            this.ContinueThread();
        }

        #endregion
    }
}