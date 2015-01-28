// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
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
            using (var engine = GameEngine.Instance)
            {
                var runner = new Perseverance(engine);

                runner.Run();
            }
        }
    }
#endif
}