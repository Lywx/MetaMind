namespace MetaMind.Engine
{
    using System;

    using LightInject;

#if WINDOWS || LINUX

    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            using (var ioC = new ServiceContainer())
            {
                ioC.RegisterFrom<GameEngineCompositionRoot>();

                var engine = ioC.GetInstance<IGameEngine>();
                ioC.RegisterInstance(engine);

                engine.Run();
            }
        }
    }

#endif
}