namespace MetaMind.Engine.Services.Console.Processors
{
    using System;
    using Commands;
    using Script.IronPython;

    public class IronPythonProcessor : ICommandProcessor
    {
        private readonly IpySession ipySession;

        public IronPythonProcessor(IpySession ipySession)
        {
            if (ipySession == null)
            {
                throw new ArgumentNullException(nameof(ipySession));
            }

            this.ipySession = ipySession;
        }

        public string Process(string buffer)
        {
            this.ipySession.EvalExpressionAsync(buffer);
           
            return string.Empty;
        }

        public IConsoleCommand Match(string incomplete)
        {
            return null;
        }
    }
}