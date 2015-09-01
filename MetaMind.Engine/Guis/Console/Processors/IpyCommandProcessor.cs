namespace MetaMind.Engine.Guis.Console.Processors
{
    using System;
    using Commands;
    using Scripting.IronPython;

    public class IpyCommandProcessor : ICommandProcessor
    {
        private readonly IpySession ipySession;

        public IpyCommandProcessor(IpySession ipySession)
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