namespace MetaMind.Engine.Core.Services.Console
{
    using System.Diagnostics;

    public static class GameConsoleSwitch
    {
        private static IMMEngineInteropService Interop => MMEngine.Service.Interop;

        public static void DebugFlush(string output, string error)
        {
            if (!string.IsNullOrEmpty(output))
            {
                Debug.WriteLine(output);
            }

            if (!string.IsNullOrEmpty(error))
            {
                Debug.WriteLine(error);
            }
        }

        public static void ConsoleFlush(string output, string error)
        {
            if (!string.IsNullOrEmpty(output))
            {
                Interop.Console.WriteLine(output, "DEBUG");
            }

            if (!string.IsNullOrEmpty(error))
            {
                Interop.Console.WriteLine(error, "ERROR");
            }
        }
    }
}