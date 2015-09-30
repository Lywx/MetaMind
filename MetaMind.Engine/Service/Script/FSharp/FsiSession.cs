namespace MetaMind.Engine.Service.Scripting.FSharp
{
    using System;
    using System.IO;
    using System.Text;
    using Microsoft.FSharp.Compiler.Interactive;
    using Microsoft.FSharp.Core;
    using Service.Console;

    public class FsiSession : QueueSession
    {
        private const string CreateSessionAsyncThreadName   = "FsiSession.CreateSessionAsync";

        private const string EvalInteractionAsyncThreadName = "FsiSession.EvalInteractionAsync";

        private const string EvalScriptAsyncThreadName      = "FsiSession.EvalScriptAsync";

        #region Fsi

        private readonly string fsiPath =
            @"C:\Program Files (x86)\Microsoft SDKs\F#\4.0\Framework\v4.0\FsiAnyCPU.exe";

        private readonly StringBuilder outBuilder;

        private readonly StringBuilder errorBuilder;

        private readonly StringReader inReader;

        private readonly StringWriter outWriter;

        private readonly StringWriter errorWriter;

        private Shell.FsiEvaluationSessionHostConfig fsiConfig;

        private Shell.FsiEvaluationSession fsiSession;

        #endregion

        public FsiSession()
        {
            this.outBuilder   = new StringBuilder();
            this.errorBuilder = new StringBuilder();

            this.inReader    = new StringReader(string.Empty);
            this.outWriter   = new StringWriter(this.outBuilder);
            this.errorWriter = new StringWriter(this.errorBuilder);

            this.CreateSessionAsync();
        }

        public bool IsVerbose { get; set; }

        public bool IsInitialized => this.fsiSession != null;

        private string[] FsiArgv => new[]{ this.fsiPath, "--nologo", "--gui-" };

        public StringBuilder Error => this.errorBuilder;

        public StringBuilder Out => this.outBuilder;

        #region Session Operations

        private void CreateSessionAsync()
        {
            this.StartThread(CreateSessionAsyncThreadName, this.CreateSession);
        }

        private void CreateSession()
        {
            this.fsiConfig = Shell.FsiEvaluationSession.GetDefaultConfiguration();
            this.fsiSession = Shell.FsiEvaluationSession.Create(
                this.fsiConfig,
                this.FsiArgv,
                this.inReader,
                this.outWriter,
                this.errorWriter,
                new FSharpOption<bool>(true));
        }

        #endregion

        #region Evaluation Operations

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">Path of script file</param>
        public void EvalScriptAsync(string path)
        {
            if (this.IsInitialized)
            {
                this.StartThread(EvalScriptAsyncThreadName, () => this.EvalScript(path));
            }
            else
            {
                this.DeferThread(EvalScriptAsyncThreadName, () => this.EvalScriptAsync(path));
            }
        }

        public void EvalScript(string path)
        {
            try
            {
                this.fsiSession.EvalScript(path);
            }
            catch (Exception e)
            {
                this.Error.Append(e);
            }
            finally
            {
                this.FlushBuffer();
            }
        }

        ///<summary>
        /// It is used to debug interactively with FsiDebugger. 
        ///</summary> 
        public object EvalExpression(string expression)
        {
            try
            {
                var result = this.fsiSession.EvalExpression(expression);

                return result.Value.ReflectionValue;
            }
            catch (Exception)
            {
                // Ignored
            }
            finally
            {
                this.FlushBuffer();
            }

            return null;
        }

        public void EvalInteractionAsync(string code)
        {
            if (this.IsInitialized)
            {
                this.StartThread(EvalInteractionAsyncThreadName, () => this.EvalInteraction(code));
            }
            else
            {
                this.DeferThread(EvalInteractionAsyncThreadName, () => this.EvalInteractionAsync(code));
            }
        }

        public void EvalInteraction(string code)
        {
            try
            {
                this.fsiSession.EvalInteraction(code);
            }
            catch (Exception e)
            {
                this.Error.Append(e);
            }
            finally
            {
                this.FlushBuffer();
            }
        }

        #endregion

        #region Output Operations

        private void ClearBuffer()
        {
            this.Out  .Clear();
            this.Error.Clear();
        }

        private void FlushBuffer()
        {
            this.FlushBuffer(this.Out.ToString(), this.Error.ToString());
        }

        private void FlushBuffer(string output, string error)
        {
            if (this.IsVerbose)
            {
                GameConsoleSwitch.ConsoleFlush(output, error);
            }
            else
            {
                GameConsoleSwitch.DebugFlush(output, error);
            }

            this.ClearBuffer();
        }

        #endregion
    }
}