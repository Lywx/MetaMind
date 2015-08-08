namespace MetaMind.Unity.Guis.Console.Commands
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
                return "Enables or disables verbose fsi session execution";
            }
        }

        public string Execute(string[] arguments)
        {
            Unity.FsiSession.Verbose = !Unity.FsiSession.Verbose;

            return "";
        }
    }
}