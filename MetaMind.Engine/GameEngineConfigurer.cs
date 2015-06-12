namespace MetaMind.Engine
{
    using Components;
    using Components.Fonts;
    using Components.Graphics;
    using Guis.Console;
    using Guis.Console.Commands;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;

    public class GameEngineConfigurer : IGameEngineConfigurer
    {
        public virtual void Configure(GameEngine engine)
        {
            // Game graphics
            var graphicsSettings = new GraphicsSettings();
            var graphics = new GameEngineGraphics(
                engine,
                graphicsSettings,
                new GraphicsManager(engine, graphicsSettings));

            // Game interop
            var game = new GameManager(engine);

            // -- Audio
            var audioSettings = @"Content\Audio\Win\Audio.xgs";
            var waveBankSettings = @"Content\Audio\Win\Wave Bank.xwb";
            var soundBankSettings = @"Content\Audio\Win\Sound Bank.xsb";

            var audioEngine = new AudioEngine(audioSettings);
            var waveBank = new WaveBank(audioEngine, waveBankSettings);
            var soundBank = new SoundBank(audioEngine, soundBankSettings);

            var audio = new AudioManager(engine, audioEngine, waveBank, soundBank)
            {
                UpdateOrder = int.MaxValue
            };

            // -- Others
            var file = new FileManager();
            var @event = new EventManager(engine) { UpdateOrder = 3 };
            var process = new ProcessManager(engine) { UpdateOrder = 4 };
            var screen = new ScreenManager(
                engine,
                new ScreenSettings(),
                graphics.SpriteBatch) { UpdateOrder = 5 };

            // -- Console
            var console = new GameConsole(
                engine,
                graphics.SpriteBatch,
                graphics.StringDrawer,
                new GameConsoleOptions
                {
                    Font            = Font.UiConsole,
                    BackgroundColor = new Color(0, 0, 0, 256 * 3 / 4)
                });
            console.AddCommand(new ResetCommand(file));
            console.AddCommand(new RestartCommand(engine));

            var interop = new GameEngineInterop(
                engine,
                game,
                audio,
                file,
                @event,
                process,
                screen,
                console);

            // Game input
            var input = new GameEngineInput(engine);

            // Game numerical
            var numerical = new GameEngineNumerical();

            // Game engine property injection
            engine.Graphics = graphics;
            engine.Interop = interop;
            engine.Input = input;
            engine.Numerical = numerical;
        }

    }
}