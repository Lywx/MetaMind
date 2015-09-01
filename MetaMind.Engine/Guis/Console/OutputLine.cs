namespace MetaMind.Engine.Guis.Console
{
    internal class OutputLine
    {
        public string Output { get; set; }

        public OutputType Type { get; }

        public OutputLine(string output, OutputType type)
        {
            this.Output = output;
            this.Type   = type;
        }

        public override string ToString()
        {
            return this.Output;
        }
    }
}
