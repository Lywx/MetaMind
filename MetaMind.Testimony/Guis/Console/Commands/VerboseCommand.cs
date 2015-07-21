namespace MetaMind.Testimony.Guis.Console.Commands
{
    using Engine.Guis.Console;

    internal class VerboseCommand : IConsoleCommand
    {
        public string Name
        {
            get
            {
                return "verbose";
            }
        }

        public string Description
        {
            get
            {
                return "Enable or disable verbose fsi session execution";
            }
        }

        public string Execute(string[] arguments)
        {
            Testimony.FsiSession.Verbose = !Testimony.FsiSession.Verbose;

            return "";
        }
    }
}