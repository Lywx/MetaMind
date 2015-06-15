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
        private const string FsiPath = @"C:\Program Files (x86)\Microsoft SDKs\F#\3.1\Framework\v4.0\FsiAnyCPU.exe";

        private readonly string[] fsiArgs = { FsiPath, "--noninteractive", "--nologo", "--gui-" };

        private readonly StringBuilder @out;

        private readonly StringBuilder error;

        private readonly StringReader inStream;

        private readonly StringWriter outStream;

        private readonly StringWriter errorStream;

        private Shell.FsiEvaluationSession fsiSession;

        private object threadLock = new object();

        private Thread threadCurrent;

        private readonly List<Thread> threadsQueued = new List<Thread>();

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
                    collectible: FSharpOption<bool>.None);
                //collectible: new FSharpOption<bool>(true));
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
            var actionName = "FsiSession.EvalScript";
            var actionNotification = "MESSAGE: Script evaluation ended.";

            if (this.fsiSession != null)
            {
                this.Start(actionName, () => this.DoEvalScript(filePath), actionNotification);
            }
            else
            {
                this.Defer(actionName, () => this.EvalScript(filePath), actionNotification);
            }
        }

        ///<summary>
        /// It is used to debug interactively with FsiDebugger. 
        ///</summary> 
        public void EvalExpression(string code)
        {
            this.fsiSession.EvalExpression(code);
#if DEBUG
            Debug.WriteLine(this.Out.ToString());
            this.Out.Clear();

            Debug.WriteLine(this.Error.ToString());
            this.Error.Clear();
#endif
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

        public void Start(string actionName, Action action, string actionNotification = null)
        {
            if (this.threadCurrent == null)
            {
                this.threadCurrent = new Thread(() => this.Process(action, actionNotification)) { Name = actionName };
                this.threadCurrent.Start();
            }
            else
            {
                this.Defer(actionName, action, actionNotification);
            }
        }

        public void Defer(string actionName, Action action, string actionNotification = null)
        {
            this.threadsQueued.Add(new Thread(() => this.Process(action, actionNotification)) { Name = actionName });
        }

        private void Process(Action action, string actionNotification)
        {
            action();

            if (this.threadsQueued.Contains(this.threadCurrent))
            {
                this.threadsQueued.Remove(this.threadCurrent);
            }

            this.threadCurrent = null;

            if (actionNotification != null)
            {
                // The last thread is removed later by this.Process(Action)
                if (this.threadsQueued.Count == 0)
                {
                    this.GameInterop.Console.WriteLine(actionNotification);
                }
            }
        }

        public void Update()
        {
            if (this.threadCurrent == null &&
                this.threadsQueued.Count != 0)
            {
                this.threadCurrent = this.threadsQueued.Last();
                this.threadCurrent.Start();
            }
        }

        #endregion
    }
}