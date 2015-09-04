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
            using (var engine = new GameEngine(@"Content"))
            {
                var configurer = new GameEngineCompositor();
                configurer.Configure(engine);

                var test = new GameEngineTest(engine);
                test.Run();
            }
        }
    }
#endif
}
