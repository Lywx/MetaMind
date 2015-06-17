namespace MetaMind.Engine.Guis.Console.Commands
{
    using System;

    using MetaMind.Engine.Components;

    internal class ResetCommand : IConsoleCommand
    {
        private readonly IFileManager file;

        private readonly GameEngine engine;

        public ResetCommand(GameEngine engine, IFileManager file)
        {
            if (engine == null)
            {
                throw new ArgumentNullException("engine");
            }

            if (file == null)
            {
                throw new ArgumentNullException("file");
            }

            this.file   = file;
            this.engine = engine;
        }

        public string Description
        {
            get
            {
                return "Resets the save files";
            }
        }

        public string Name
        {
            get
            {
                return "reset";
            }
        }

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