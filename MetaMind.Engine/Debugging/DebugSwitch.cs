namespace MetaMind.Engine.Debugging
{
    using System.Diagnostics;
    using Service;

    public static class DebugSwitch
    {
        private static IGameInteropService Interop => GameEngine.Service.Interop;

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