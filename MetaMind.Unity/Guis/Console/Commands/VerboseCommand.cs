namespace MetaMind.Unity.Guis.Console.Commands
{
    using Engine.Guis.Console.Commands;

    internal class VerboseCommand : IConsoleCommand
    {
        public string Name => "verbose";

        public string Description => "Enables or disables verbose fsi session execution";

        public string Execute(string[] arguments)
        {
            Unity.FsiSession.IsVerbose = !Unity.FsiSession.IsVerbose;

            return "";
        }
    }
}