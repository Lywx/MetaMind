namespace MetaMind.Session
{
    using System;
    using Engine;
    using Engine.Core;

#if WINDOWS || LINUX

    /// <summary>
    ///     The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            var configurer = new MMSessionConfigurer();
            var builder = new MMEngineBuilder(configurer);

            using (var engine = builder.Create())
            {
                using (var session = new MMSessionGame(engine))
                {
                    session.Run();
                }
            }
        }
    }
#endif
}
