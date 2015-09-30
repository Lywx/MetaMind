namespace MetaMind.Engine.Service.Console.Commands.Coreutils
{
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
