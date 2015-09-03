namespace MetaMind.Engine.Guis.Console.Commands
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

        public string Name => "GameEngine.restart";

        public string Description => "Restarts the engine";

        public string Execute(string[] arguments)
        {
            this.engine.Restart();

            return "";
        }
    }
}
