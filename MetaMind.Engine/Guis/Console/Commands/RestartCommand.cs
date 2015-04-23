namespace MetaMind.Engine.Guis.Console.Commands
{
    using System;

    internal class RestartCommand : IConsoleCommand
    {
        private readonly IGameEngine engine;

        public string Name
        {
            get
            {
                return "restart";
            }
        }

        public string Description
        {
            get
            {
                return "Restarts the engine";
            }
        }

        public RestartCommand(IGameEngine engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException("engine");
            }

            this.engine = engine;
        }

        public string Execute(string[] arguments)
        {
            this.engine.Restart();

            return "";
        }
    }
}