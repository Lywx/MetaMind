namespace MetaMind.Engine.Services.Console.Commands.Coreutils
{
    using Entities;
    using Entities.Bases;

    internal class RestartCommand : MMEntity, IConsoleCommand
    {
        public RestartCommand()
        {
        }

        public string Name => "restart";

        public string Description => "Restarts the engine";

        public string Execute(string[] arguments)
        {
            this.GlobalInterop.Engine.Restart();

            return string.Empty;
        }
    }
}
