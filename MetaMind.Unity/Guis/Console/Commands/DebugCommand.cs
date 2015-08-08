namespace MetaMind.Unity.Guis.Console.Commands
{
    using Engine.Guis.Console;

    internal class DebugCommand : IConsoleCommand
    {
        public string Name
        {
            get { return "debug"; }
        }

        public string Description
        {
            get
            {
                return "Enables or disables error information of fsi session execution";
            }
        }

        public string Execute(string[] arguments)
        {
            Unity.FsiSession.Debugging = !Unity.FsiSession.Debugging;

            return "";
        }
    }
}
