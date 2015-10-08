namespace MetaMind.Engine.Services.Console.Commands.Coreutils
{
    using Entities;

    internal class RestartCommand : MMEntity, IConsoleCommand
    {
        public RestartCommand()
        {
        }

        public string Name => "restart";

        public string Description => "Restarts the engine";

        public string Execute(string[] arguments)
        {
            this.Interop.Engine.Restart();

            return string.Empty;
        }
    }
}
