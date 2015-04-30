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
            using (var engine = new GameEngine())
            {
                var test = new Test(engine);
                test.Run();
            }
        }
    }
#endif
}
