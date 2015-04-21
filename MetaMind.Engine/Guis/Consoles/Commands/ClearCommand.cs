namespace MonoGameConsole.Commands
{
    internal class ClearCommand : IConsoleCommand
    {
        public string Name
        {
            get
            {
                return "clear";
            }
        }

        public string Description
        {
            get
            {
                return "Clears the console output";
            }
        }

        private InputProcessor processor;

        public ClearCommand(InputProcessor processor)
        {
            this.processor = processor;
        }

        public string Execute(string[] arguments)
        {
            this.processor.Out.Clear();

            return "";
        }
    }
}