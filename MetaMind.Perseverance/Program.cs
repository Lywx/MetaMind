// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance
{
    using System;
    using System.Linq;

    using MetaMind.Engine;

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
            using (var engine = GameEngine.GetInstance())
            {
                var fullscreen = args.Count() != 0 && args[0] == "--fullscreen";
                var runner = new Perseverance(engine, fullscreen);

                runner.Run();
            }
        }
    }
#endif
}