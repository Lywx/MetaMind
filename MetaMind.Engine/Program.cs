namespace MetaMind.Engine
{
    using System;

#if WINDOWS || LINUX

    public static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            var builder = new GameEngineBuilder();

            using (var engine = builder.Create(@"Content"))
            {
                engine.Run();
            }
        }
    }

#endif
}