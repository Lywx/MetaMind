namespace MetaMind.Engine.Core.Services.Console.Commands.Coreutils
{
    using Entity.Common;

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
