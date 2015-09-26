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
            var builder = new GameEngineBuilder();

            using (var engine = builder.Create())
            {
                var test = new GameEngineTest(engine);
                test.Run();
            }
        }
    }
#endif
}
