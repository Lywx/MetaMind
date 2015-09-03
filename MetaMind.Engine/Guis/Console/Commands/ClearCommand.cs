namespace MetaMind.Engine.Guis.Console.Commands
{
    internal class ClearCommand : IConsoleCommand
    {
        private readonly GameConsoleModule module;

        public ClearCommand(GameConsoleModule module)
        {
            this.module = module;
        }

        public string Name => "clear";

        public string Description => "Clears the console output";

        public string Execute(string[] arguments)
        {
            this.module.Clear();

            return "";
        }
    }
}
