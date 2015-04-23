namespace MetaMind.Engine.Guis.Console.Commands
{
    using System;

    using MetaMind.Engine.Components;

    internal class ResetCommand : IConsoleCommand
    {
        private readonly FileManager file;

        public ResetCommand(FileManager file)
        {
            if (file == null)
            {
                throw new ArgumentNullException("file");
            }

            this.file = file;
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
            this.file.DeleteSaveDirectory();

            return "";
        }
    }
}