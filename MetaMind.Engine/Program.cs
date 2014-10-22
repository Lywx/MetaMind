using System;

namespace MetaMind.Engine
{
#if WINDOWS || XBOX

    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            using (var engine = new Engine())
            {
                engine.Run();
            }
        }
    }

#endif
}