// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Session
{
    using System;
    using Engine;

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
            var configurer = new SessionConfigurer();
            var builder    = new MMEngineBuilder(configurer);

            using (var engine = builder.Create())
            {
                var testimony = new SessionGame(engine);
                testimony.Run();
            }
        }
    }
#endif
}