namespace MetaMind.Engine.Core.Services.Console.Commands.Coreutils
{
    using Entity.Common;

    internal class ResetCommand : MMEntity, IConsoleCommand
    {
        public ResetCommand()
        {
        }

        public string Description => "Resets the engine save files";

        public string Name => "reset";

        public string Execute(string[] arguments)
        {
            // Must call restart first, because it will synchronously save the current session
            this.GlobalInterop.Engine.Restart();

            // Delete the save file before next session is started
            this.GlobalInterop.File.DeleteSaveDirectory();

            return "";
        }
    }
}