namespace MetaMind.Engine.Console
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
}
