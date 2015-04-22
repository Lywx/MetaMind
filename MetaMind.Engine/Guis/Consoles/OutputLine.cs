namespace MetaMind.Engine.Guis.Consoles
{
    internal enum OutputLineType
    {
        Command,

        Output
    }

    internal class OutputLine
    {
        public string Output { get; set; }

        public OutputLineType Type { get; set; }

        public OutputLine(string output, OutputLineType type)
        {
            this.Output = output;
            this.Type = type;
        }

        public override string ToString()
        {
            return this.Output;
        }
    }
}
