namespace MetaMind.Engine.Guis.Console.Commands
{
    using System;

    using Components;

    internal class ResetCommand : IConsoleCommand
    {
        private readonly IFileManager file;

        private readonly GameEngine engine;

        public ResetCommand(GameEngine engine, IFileManager file)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            this.file   = file;
            this.engine = engine;
        }

        public string Description => "Resets the save files";

        public string Name => "GameEngine.reset";

        public string Execute(string[] arguments)
        {
            // Must call restart first, because it will synchronously save the current session
            this.engine.Restart();

            // Delete the save file before next session is started
            this.file.DeleteSaveDirectory();

            return "";
        }
    }
}