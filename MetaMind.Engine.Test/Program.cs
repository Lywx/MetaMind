namespace MetaMind.EngineTest
{
    using System;

    using MetaMind.Engine;

#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            var builder = new MMEngineBuilder();

            using (var engine = builder.Create())
            {
                var test = new MMEngineTest(engine);
                test.Run();
            }
        }
    }
#endif
}
