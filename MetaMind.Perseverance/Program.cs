#region Using Statements
using System;


#endregion

namespace MetaMind.Perseverance
{
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
        static void Main()
        {
            using ( var engine = new Engine.GameEngine() )
            {
                var runner = new Perseverance( engine );
                runner.Run();
            }
        }
    }
#endif
}
