namespace MetaMind.Engine
{
    using System;
    using Component;
    using Component.File;
    using Component.Graphics;
    using Component.Interop;
    using Component.Interop.Event;
    using Component.Interop.Process;
    using Console;
    using Microsoft.Xna.Framework;

    public class GameEngineBuilder : IGameEngineBuilder
    {
        public GameEngineBuilder()
        {

        }

        public GameEngineBuilder(IGameEngineConfigurer configurer)
        {
            if (configurer == null)
            {
                throw new ArgumentNullException(nameof(configurer));
            }

            this.Configurer = configurer;
        }

        private IGameEngineConfigurer Configurer { get; }

        public GameEngine Create(string content)
        {
            var engine = new GameEngine(content);

            this.Configure(engine);
            this.Configurer?.Configure(engine);

            return engine;
        }

        private void Configure(GameEngine engine)
        {
            // Game graphics
            var graphicsSettings = new GraphicsSettings();
            var graphicsManager = new GraphicsManager(engine, graphicsSettings);
            var graphics = new GameEngineGraphics(
                engine,
                graphicsSettings,
                graphicsManager);

            // Game engine property injection
            engine.Graphics = graphics;
            engine.Interop = new GameEngineInterop(
                engine,
                new GameManager(engine),
                new EventManager(engine)
                {
                    UpdateOrder = 3
                },
                new ProcessManager(engine)
                {
                    UpdateOrder = 4
                },
                new ScreenManager(
                    engine,
                    new ScreenSettings(),
                    graphics.SpriteBatch)
                {
                    UpdateOrder = 5
                },
                new GameConsole(
                    new GameConsoleSettings
                    {
                        // TODO: UI should register font name
                        Font = Font.UiConsole,
                        Height = graphicsSettings.Height - 50,
                        BackgroundColor = new Color(0, 0, 0, 256 * 3 / 4),
                        PastErrorColor = Color.Red,
                        PastDebugColor = Color.Yellow,
                    },
                    engine,
                    graphics.SpriteBatch,
                    graphics.Renderer));

            engine.Input     = new GameEngineInput(engine);
            engine.Numerical = new GameEngineNumerical();
        }
    }
}