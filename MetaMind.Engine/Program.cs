namespace MetaMind.Engine
{
    using System;

#if WINDOWS || LINUX

    public static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            var builder = new MMEngineBuilder();

            using (var engine = builder.Create())
            {
                engine.Run();
            }
        }
    }

#endif
}