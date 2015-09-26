// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Unity
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
            var configurer = new UnityEngineConfigurer();
            var builder    = new GameEngineBuilder(configurer);

            using (var engine = builder.Create())
            {
                var testimony = new Unity(engine);
                testimony.Run();
            }
        }
    }
#endif
}