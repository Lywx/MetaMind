namespace MetaMind.Acutance
{
    using System;
    using System.Linq;

    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            using (var engine = new Engine.GameEngine())
            {
                var fullscreen = args.Count() != 0 && args[0] == "--fullscreen";
                var runner     = new Acutance(engine, fullscreen);

                runner.Run();
            }
        }
    }
}