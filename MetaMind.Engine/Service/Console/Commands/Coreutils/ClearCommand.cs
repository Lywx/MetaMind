namespace MetaMind.Engine.Service.Console.Commands.Coreutils
{
    internal class ClearCommand : IConsoleCommand
    {
        private readonly GameConsole module;

        public ClearCommand(GameConsole module)
        {
            this.module = module;
        }

        public string Name => "clear";

        public string Description => "Clears the console output";

        public string Execute(string[] arguments)
        {
            this.module.ClearOutput();

            return "";
        }
    }
}
