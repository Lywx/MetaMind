namespace MetaMind.Engine.Services.Console.Commands
{
    internal class CommandLine
    {
        public string Command { get; set; }

        public CommandType Type { get; }

        public CommandLine(string command, CommandType type)
        {
            this.Command = command;
            this.Type    = type;
        }

        public override string ToString()
        {
            return this.Command;
        }
    }

    internal class VisualCommandLine : CommandLine
    {
        public VisualCommandLine(string command, CommandType type)
            : base(command, type)
        {
        }
    }
}
