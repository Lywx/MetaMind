namespace MetaMind.Engine.Scripting.FSharp
{
    using Guis.Console.Commands;

    public class FsiSessionVerboseCommand : IConsoleCommand
    {
        private readonly FsiSession fsiSession;

        public FsiSessionVerboseCommand(FsiSession fsiSession)
        {
            this.fsiSession = fsiSession;
        }

        public string Name => "FsiSession.verbose";

        public string Description => "Enables or disables verbose fsi session execution";

        public string Execute(string[] arguments)
        {
            this.fsiSession.IsVerbose = !this.fsiSession.IsVerbose;

            return "";
        }
    }
}