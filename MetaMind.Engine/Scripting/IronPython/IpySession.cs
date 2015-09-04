using Python = IronPython.Hosting.Python;

namespace MetaMind.Engine.Scripting.IronPython
{
    using System;
    using System.IO;
    using System.Text;
    using Microsoft.Scripting;
    using Microsoft.Scripting.Hosting;

    public class IpySession : QueueSession
    {
        private const string CreateSessionAsyncThreadName  = "IpySession.CreateSessionAsync";

        private const string EvalExpressionAsyncThreadName = "IpySession.EvalExpressionAsync";

        private ScriptEngine engine;

        private ScriptScope scope;

        private MemoryStream outputStream;

        private StreamWriter outputStreamWriter;

        public IpySession()
        {
            this.CreateSessionAsync();
        }

        public bool IsInitialized => this.engine != null && this.scope != null;

        public bool IsVerbose { get; set; } = true;

        public StringBuilder Error { get; private set; }

        public StringBuilder Out { get; private set; }

        #region Session Operations

        private void CreateSessionAsync()
        {
            this.StartThread(CreateSessionAsyncThreadName, this.CreateSession);
        }

        private void CreateSession()
        {
            this.engine = Python.CreateEngine();
            this.scope  = this.engine.CreateScope();

            this.CreateOutputStream();
            this.CreateBuffer();
            this.CreateVariables();
        }

        private void CreateVariables()
        {
            this.SetVariable("GameEngine", this.Interop.Engine);
            this.SetVariable("Game", this.Interop.Game.Game);
            this.EvalExpressionAsync(
@"import clr; clr.AddReference(""MetaMind.Unity""); from MetaMind.Unity import Unity");

            //this.SetVariable("s", new Action<string[]>(new ClearCommand().Execute(new[] {""})));
        }

        public void SetVariable(string name, object value)
        {
            this.scope.SetVariable(name, value);
        }

        #endregion

        #region Evaluation Operations

        public void EvalExpressionAsync(string expression)
        {
            if (this.IsInitialized)
            {
                this.StartThread(
                    EvalExpressionAsyncThreadName,
                    () => this.EvalExpression(expression));
            }
            else
            {
                this.DeferThread(
                    EvalExpressionAsyncThreadName,
                    () => this.EvalExpressionAsync(expression));
            }
        }

        public void EvalExpression(string expression)
        {
            try
            {
                var source = this.engine.CreateScriptSourceFromString(
                    expression,
                    SourceCodeKind.Statements);
                var compiled = source.Compile();

                compiled.Execute(this.scope);
            }
            catch (Exception e)
            {
                this.Error.Append(e);
            }
            finally
            {
                this.ReadOutput();
            }

            this.FlushBuffer();
        }

        #endregion

        #region Output Operations

        private void ClearBuffer()
        {
            this.Out  .Clear();
            this.Error.Clear();
        }

        private void ClearStream()
        {
            this.DisposeOutputStream();
            this.CreateOutputStream();
        }

        private void CreateOutputStream()
        {
            this.outputStream       = new MemoryStream();
            this.outputStreamWriter = new StreamWriter(this.outputStream);

            this.engine.Runtime.IO.SetOutput(
                this.outputStream,
                this.outputStreamWriter);
        }

        private void CreateBuffer()
        {
            this.Error = new StringBuilder();
            this.Out   = new StringBuilder();
        }

        private void FlushBuffer()
        {
            this.FlushBuffer(this.Out.ToString(), this.Error.ToString());
        }

        private void FlushBuffer(string output, string error)
        {
            if (this.IsVerbose)
            {
                Diagnostics.ConsoleWriteLine(output, error);
            }
#if DEBUG
            else
            {
                Diagnostics.DebugWriteLine(output, error);
            }
#endif
            this.ClearBuffer();
            this.ClearStream();
        }

        private void ReadOutput()
        {
            this.Out.Append(this.ReadStream(this.outputStream).Trim());
        }

        private string ReadStream(MemoryStream memoryStream)
        {
            var length = (int)memoryStream.Length;
            var bytes  = new byte[length];

            memoryStream.Seek(0, SeekOrigin.Begin);
            memoryStream.Read(bytes, 0, length);

            return Encoding.GetEncoding("utf-8")
                           .GetString(bytes, 0, length);
        }

        #endregion

        #region IDisposable

        private bool IsDisposed { get; set; }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (!this.IsDisposed)
                    {
                        this.DisposeOutputStream();
                    }

                    this.IsDisposed = true;
                }
            }
            catch
            {
                // Ignored
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        private void DisposeOutputStream()
        {
            if (this.outputStreamWriter != null)
            {
                this.outputStreamWriter.Dispose();
                this.outputStreamWriter = null;
                this.outputStream = null;
            }
        }

        #endregion
    }
}