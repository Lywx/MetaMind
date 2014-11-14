#region Using Statements
using System;


#endregion

namespace MetaMind.Perseverance
{
    using System.Linq;

#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            using (var engine = new Engine.GameEngine())
            {
                var fullscreen = args.Count() != 0 && args[0] == "--fullscreen";
                var runner     = new Perseverance(engine, fullscreen);

                runner.Run();
            }
        }
    }
#endif
}
