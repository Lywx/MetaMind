namespace MetaMind.Engine.Components
{
    using System;

    using MetaMind.Engine.Guis.Console;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;

    public partial class GameEngineInterop : IGameInterop
    {
        public IAudioManager Audio { get; private set; }

        public GameConsole Console { get; set; }

        public ContentManager Content { get; private set; }

        public IFileManager File { get; private set; }

        public GameEngine Engine { get; private set; }

        public IEventManager Event { get; private set; }

        public IGameManager Game { get; private set; }

        public IProcessManager Process { get; private set; }

        public IScreenManager Screen { get; private set; }

        public ISaveManager Save { get; set; }

        public GameEngineInterop(GameEngine engine, IGameManager game, IAudioManager audio, IFileManager file, IEventManager @event, IProcessManager process, IScreenManager screen, GameConsole console)
        {
            if (engine == null)
            {
                throw new ArgumentNullException("engine");
            }

            if (game == null)
            {
                throw new ArgumentNullException("game");
            }

            if (audio == null)
            {
                throw new ArgumentNullException("audio");
            }

            if (file == null)
            {
                throw new ArgumentNullException("file");
            }

            if (@event == null)
            {
                throw new ArgumentNullException("@event");
            }

            if (process == null)
            {
                throw new ArgumentNullException("process");
            }

            if (screen == null)
            {
                throw new ArgumentNullException("screen");
            }

            if (console == null)
            {
                throw new ArgumentNullException("console");
            }

            this.Engine = engine;

            this.Audio   = audio;
            this.File    = file;
            this.Event   = @event;
            this.Game    = game;
            this.Process = process;
            this.Screen  = screen;

            this.Console = console;

            this.Content = engine.Content;
        }
    }

    public partial class GameEngineInterop
    {
        public void Initialize()
        {
            // Initialize components that aren't initialized during GameComponents initialization
        }

        public void UpdateInput(GameTime gameTime)
        {
            this.Screen.UpdateInput(gameTime);
        }

        public void OnExiting()
        {
            this.Screen.OnExiting();
            this.Game  .OnExiting();
        }
    }
}