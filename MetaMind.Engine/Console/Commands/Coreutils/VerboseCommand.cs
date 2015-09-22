namespace MetaMind.Engine.Console.Commands.Coreutils
{
    using Scripting.FSharp;

    public class VerboseCommand : IConsoleCommand
    {
        private readonly FsiSession fsiSession;

        // TODO(Minor): Not very good dependency injection, maybe I can provide a better abstraction for engine debugging rather than fsi session?
        public VerboseCommand(FsiSession fsiSession)
        {
            this.fsiSession = fsiSession;
        }

        public string Name => "verbose";

        public string Description => "Enables or disables verbose scripting";

        public string Execute(string[] arguments)
        {
            this.fsiSession.IsVerbose = !this.fsiSession.IsVerbose;

            return "";
        }
    }
}