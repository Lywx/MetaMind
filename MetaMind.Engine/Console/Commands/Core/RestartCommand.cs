namespace MetaMind.Engine.Console.Commands.Core
{
    using System;

    internal class RestartCommand : IConsoleCommand
    {
        private readonly IGameEngine engine;

        public RestartCommand(IGameEngine engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            this.engine = engine;
        }

        public string Name => "engine-restart";

        public string Description => "Restarts the engine";

        public string Execute(string[] arguments)
        {
            this.engine.Restart();

            return "";
        }
    }
}
