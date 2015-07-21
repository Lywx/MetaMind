namespace MetaMind.Testimony.Guis.Console.Commands
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
                return "Enable or disable error information of fsi session execution";
            }
        }

        public string Execute(string[] arguments)
        {
            Testimony.FsiSession.Debugging = !Testimony.FsiSession.Debugging;

            return "";
        }
    }
}
