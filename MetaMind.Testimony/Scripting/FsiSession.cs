namespace MetaMind.Testimony.Scripting
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;

    using Concepts;
    using Engine;
    using Microsoft.FSharp.Compiler.Interactive;
    using Microsoft.FSharp.Core;

    public class FsiSession : GameEntity, IInnerUpdatable
    {
        #region Fsi

        private const string FsiPath =
            @"C:\Program Files (x86)\Microsoft SDKs\F#\3.1\Framework\v4.0\FsiAnyCPU.exe";

        private readonly string[] fsiArgs =
        {
            FsiPath,
            "--noninteractive",
            "--nologo",
            "--gui-"
        };

        private readonly StringBuilder @out;

        private readonly StringBuilder error;

        private readonly StringReader inStream;

        private readonly StringWriter outStream;

        private readonly StringWriter errorStream;

        private Shell.FsiEvaluationSession fsiSession;

        public StringBuilder Error
        {
            get { return this.error; }
        }

        public StringBuilder Out
        {
            get { return this.@out; }
        }

        #endregion

        #region Thread

        private Thread threadCurrent;

        private readonly List<Thread> threadsQueued = new List<Thread>();

        #endregion

        public FsiSession()
        {
            this.@out = new StringBuilder();
            this.error = new StringBuilder();

            this.inStream = new StringReader("");
            this.outStream = new StringWriter(this.@out);
            this.errorStream = new StringWriter(this.error);

            this.Start("FsiSession.CreateSession", () =>
            {
                var fsiConfig   = Shell.FsiEvaluationSession.GetDefaultConfiguration();
                this.fsiSession = Shell.FsiEvaluationSession.Create(
                    fsiConfig: fsiConfig,
                    argv: this.fsiArgs,
                    inReader: this.inStream,
                    outWriter: this.outStream,
                    errorWriter: this.errorStream,

                    // Uncomment to disable reflection in shell

                    // collectible: FSharpOption<bool>.None);

                collectible: new FSharpOption<bool>(true));
            });
        }

        #region Events

        /// <summary>
        /// End of a series of asynchronous session
        /// </summary>>
        public event EventHandler Stopped;
            
        /// <summary>
        /// Start of a series of asynchronous session
        /// </summary>>
        public event EventHandler Started;

        private void OnStopped()
        {
            // Safe threading
            var stopped = this.Stopped;

            if (stopped != null)
            {
                stopped(this, EventArgs.Empty);
            }
        }

        private void OnStarted()
        {
            // Safe threading
            var started = this.Started;

            if (started != null)
            {
                started(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Fsi Evaluation

        public void EvalScript(string filePath)
        {
            var actionName = "FsiSession.EvalScript";

            if (this.fsiSession != null)
            {
                this.Start(actionName, () => this.DoEvalScript(filePath));
            }
            else
            {
                this.Defer(actionName, () => this.EvalScript(filePath));
            }
        }

        ///<summary>
        /// It is used to debug interactively with FsiDebugger. 
        ///</summary> 
        public object EvalExpression(string code)
        {
            var expression = this.fsiSession.EvalExpression(code);
#if DEBUG
            Debug.WriteLine(this.Out.ToString());
            this.Out.Clear();

            Debug.WriteLine(this.Error.ToString());
            this.Error.Clear();
#endif

            return expression.Value.ReflectionValue;
        }

        ///<summary>
        /// It is used to debug interactively with FsiDebugger. 
        ///</summary> 
        public void EvalInteraction(string code)
        {
            this.fsiSession.EvalInteraction(code);
#if DEBUG
            Debug.WriteLine(this.Out.ToString());
            this.Out.Clear();

            Debug.WriteLine(this.Error.ToString());
            this.Error.Clear();
#endif
        }

        private void DoEvalScript(string filePath)
        {
            try
            {
                this.fsiSession.EvalScript(filePath);
#if DEBUG
                Debug.WriteLine(this.Out.ToString());
                this.Out.Clear();

                Debug.WriteLine(this.Error.ToString());
                this.Error.Clear();
#endif
            }
            catch (Exception)
            {
                this.GameInterop.Console.WriteLine(
                    string.Format("ERROR: Script evaluation at \"{0}\" failed.", filePath));
            }
        }

        #endregion

        #region Thread

        protected void Continue()
        {
            if (this.threadCurrent == null &&
                this.threadsQueued.Count != 0)
            {
                this.threadCurrent = this.threadsQueued.First();
                this.threadCurrent.Start();
            }
        }

        protected void Defer(string actionName, Action action)
        {
            this.threadsQueued.Add(new Thread(() => this.Process(action)) { Name = actionName });
        }

        protected void Start(string actionName, Action action)
        {
            if (this.threadCurrent == null)
            {
                this.threadCurrent = new Thread(() => this.Process(action)) { Name = actionName };
                this.threadCurrent.Start();
            }
            else
            {
                this.Defer(actionName, action);
            }
        }

        protected new void Process(Action action)
        {
            if (this.threadsQueued.Count == 0)
            {
                this.OnStarted();
            }

            action();

            if (this.threadsQueued.Contains(this.threadCurrent))
            {
                this.threadsQueued.Remove(this.threadCurrent);
            }

            this.threadCurrent = null;

            if (this.threadsQueued.Count == 0)
            {
                this.OnStopped();
            }
        }

        #endregion

        #region Update

        public void Update()
        {
            this.Continue();
        }

        #endregion
    }
}