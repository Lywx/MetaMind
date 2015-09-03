namespace MetaMind.Engine.Scripting
{
    using System.Diagnostics;
    using Services;

    public static class Diagnostics
    {
        public static void DebugWriteLine(string output, string error)
        {
            Debug.WriteLine(output);
            Debug.WriteLine(error);
        }

        public static void ConsoleWriteLine(string output, string error)
        {
            var console = GameInterop.Console;

            if (!string.IsNullOrEmpty(output))
            {
                console.WriteLine(output, "DEBUG");
            }

            if (!string.IsNullOrEmpty(error))
            {
                console.WriteLine(error, "ERROR");
            }
        }

        private static IGameInteropService GameInterop => GameEngine.Service.Interop;
    }
}