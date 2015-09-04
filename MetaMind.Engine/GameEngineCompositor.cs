namespace MetaMind.Engine
{
    using Components;
    using Components.Fonts;
    using Components.Graphics;
    using Console;
    using Console.Commands.Core;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;

    public class GameEngineCompositor : IGameEngineCompositor
    {
        public virtual void Configure(GameEngine engine)
        {
            // ------------
            // Game graphics
            // ------------
            var graphicsSettings = new GraphicsSettings();
            var graphicsManager  = new GraphicsManager(engine, graphicsSettings);
            var graphics = new GameEngineGraphics(
                engine,
                graphicsSettings,
                graphicsManager);

            // ------------
            // Game interop
            // ------------
            var gameManager = new GameManager(engine);

            // Audio
            var audioManager = this.CreateAudioManager(engine);

            // Others
            var file    = new FileManager();
            var @event  = new EventManager(engine) { UpdateOrder = 3 };
            var process = new ProcessManager(engine) { UpdateOrder = 4 };
            var screen  = new ScreenManager(
                engine,
                new ScreenSettings(),
                graphics.SpriteBatch) { UpdateOrder = 5 };

            // Console
            var consoleSettings = new GameConsoleSettings
            {
                Font            = Font.UiConsole,
                Height          = graphicsSettings.Height - 50,
                BackgroundColor = new Color(0, 0, 0, 256 * 3 / 4), 
                PastErrorColor  = Color.Red,
                PastDebugColor  = Color.Yellow,
            };
            var console = new GameConsole(consoleSettings, engine, graphics.SpriteBatch, graphics.StringDrawer);
#if DEBUG
            console.AddCommand(new ResetCommand(engine, file));
#endif
            console.AddCommand(new RestartCommand(engine));

            var interop = new GameEngineInterop(
                engine,
                gameManager,
                audioManager,
                file,
                @event,
                process,
                screen,
                console);

            // ------------
            // Game input
            // ------------
            var input = new GameEngineInput(engine);

            // ------------
            // Game numerical
            // ------------
            var numerical = new GameEngineNumerical();

            // Game engine property injection
            engine.Graphics  = graphics;
            engine.Interop   = interop;
            engine.Input     = input;
            engine.Numerical = numerical;
        }

        private AudioManager CreateAudioManager(GameEngine engine)
        {
            var audioSettings     = @"Content\Audio\Win\Audio.xgs";
            var waveBankSettings  = @"Content\Audio\Win\Wave Bank.xwb";
            var soundBankSettings = @"Content\Audio\Win\Sound Bank.xsb";

            var audioEngine = new AudioEngine(audioSettings);
            var waveBank    = new WaveBank(audioEngine, waveBankSettings);
            var soundBank   = new SoundBank(audioEngine, soundBankSettings);

            var audioManager = new AudioManager(
                engine,
                audioEngine,
                waveBank,
                soundBank)
            {
                UpdateOrder = int.MaxValue
            };

            return audioManager;
        }
    }
}