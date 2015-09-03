namespace MetaMind.Engine.Console.Commands.Core
{
    using Scripting.FSharp;

    public class VerboseCommand : IConsoleCommand
    {
        private readonly FsiSession fsiSession;

        public VerboseCommand(FsiSession fsiSession)
        {
            this.fsiSession = fsiSession;
        }

        public string Name => "engine-verbose";

        public string Description => "Enables or disables verbose scripting";

        public string Execute(string[] arguments)
        {
            this.fsiSession.IsVerbose = !this.fsiSession.IsVerbose;

            return "";
        }
    }
}