namespace MetaMind.Engine.Components
{
    using System;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;

    public class GameEngineInterop : IGameInterop
    {
        public IAudioManager Audio { get; private set; }

        public ContentManager Content { get; private set; }

        public FolderManager Folder { get; private set; }

        public IGameEngine Engine { get; private set; }

        public IEventManager Event { get; private set; }

        public IGameManager Game { get; private set; }

        public IProcessManager Process { get; private set; }

        public IScreenManager Screen { get; private set; }

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

            this.Audio   = new AudioManager(engine, settingsFile, waveBankFile, soundBankFile);
            this.Event   = new EventManager(engine, 4);
            this.Process = new ProcessManager(engine, 5);
            this.Screen  = new ScreenManager(engine, new ScreenSettings(), engine.Graphics.SpriteBatch, 3);

            this.Content = engine.Content;
            this.Folder  = new FolderManager();

            this.Game = new GameManager(engine);
        }

        public void Initialize()
        {
            this.Audio  .Initialize();
            this.Event  .Initialize();
            this.Process.Initialize();
            this.Screen .Initialize();
        }

        public void UpdateInput(GameTime gameTime)
        {
            this.Screen.UpdateInput(gameTime);
        }
    }
}