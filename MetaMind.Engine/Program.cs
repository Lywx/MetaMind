using System;

namespace MetaMind.Engine
{
#if WINDOWS || LINUX

    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            using (var engine = GameEngine.GetInstance())
            {
                engine.Run();
            }
        }
    }

#endif
}