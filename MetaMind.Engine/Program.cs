namespace MetaMind.Engine
{
    using System;
    using Components;
    using Components.Fonts;
    using Components.Graphics;
    using Microsoft.Xna.Framework.Audio;

#if WINDOWS || LINUX

    public static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            using (var engine = new GameEngine(@"Content"))
            {
                var configurer = new GameEngineConfigurer();
                configurer.Configure(engine);

                engine.Run();
            }
        }
    }

#endif
}