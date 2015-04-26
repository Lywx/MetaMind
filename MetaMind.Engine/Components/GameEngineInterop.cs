namespace MetaMind.Engine.Components
{
    using System;

    using MetaMind.Engine.Guis.Console;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;

    public class GameEngineInterop : IGameInterop
    {
        public IAudioManager Audio { get; private set; }

        public GameConsole Console { get; set; }

        public ContentManager Content { get; private set; }

        public FileManager File { get; private set; }

        public GameEngine Engine { get; private set; }

        public IEventManager Event { get; private set; }

        public IGameManager Game { get; private set; }

        public IProcessManager Process { get; private set; }

        public IScreenManager Screen { get; private set; }

        public ISaveManager Save { get; set; }

        public void OnExiting()
        {
            this.Game.OnExiting();
        }

        public GameEngineInterop(GameEngine engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException("engine");
            }

            this.Engine = engine;

            var settingsFile  = @"Content\Audio\Audio.xgs";
            var waveBankFile  = @"Content\Audio\Wave Bank.xwb";
            var soundBankFile = @"Content\Audio\Sound Bank.xsb";

            this.Audio   = new AudioManager(engine, settingsFile, waveBankFile, soundBankFile, int.MaxValue);
            this.Event   = new EventManager(engine, 4);
            this.Process = new ProcessManager(engine, 5);
            this.Screen  = new ScreenManager(engine, new ScreenSettings(), engine.Graphics.SpriteBatch, 3);

            this.Content = engine.Content;
            this.File    = new FileManager();

            this.Game = new GameManager(engine);
        }
        
        public void Initialize()
        {
            // Initialize components that aren't initialized during GameComponents initialization
        }

        public void UpdateInput(GameTime gameTime)
        {
            this.Screen.UpdateInput(gameTime);
        }
    }
}