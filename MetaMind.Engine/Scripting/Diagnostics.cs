namespace MetaMind.Engine.Scripting
{
    using System.Diagnostics;

    public static class Diagnostics
    {
        public static void DebugWriteLine(string output, string error)
        {
            Debug.WriteLine(output);
            Debug.WriteLine(error);
        }

        public static void ConsoleWriteLine(string output, string error)
        {
            var console = GameEngine.Service.Interop.Console;

            if (!string.IsNullOrEmpty(output))
            {
                console.WriteLine(output, "DEBUG");
            }

            if (!string.IsNullOrEmpty(error))
            {
                console.WriteLine(error, "ERROR");
            }
        }
    }
}