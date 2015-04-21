namespace MetaMind.Engine
{
    using System;

#if WINDOWS || LINUX

    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            using (var engine = new GameEngine()) engine.Run();
        }
    }

#endif
}