namespace MetaMind.Engine.Components
{
    using System;
    using Guis.Console;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;

    public partial class GameEngineInterop : GameControllableComponent, IGameInterop
    {
        public IAudioManager Audio { get; private set; }

        public GameConsole Console { get; set; }

        public ContentManager Content { get; private set; }

        public IFileManager File { get; private set; }

        public GameEngine Engine { get; private set; }

        public IEventManager Event { get; private set; }

        public new IGameManager Game { get; private set; }

        public IProcessManager Process { get; private set; }

        public IScreenManager Screen { get; private set; }

        public ISaveManager Save { get; set; }

        public GameEngineInterop(GameEngine engine, IGameManager game, IAudioManager audio, IFileManager file, IEventManager @event, IProcessManager process, IScreenManager screen, GameConsole console)
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            if (audio == null)
            {
                throw new ArgumentNullException(nameof(audio));
            }

            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (@event == null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            if (process == null)
            {
                throw new ArgumentNullException(nameof(process));
            }

            if (screen == null)
            {
                throw new ArgumentNullException(nameof(screen));
            }

            if (console == null)
            {
                throw new ArgumentNullException(nameof(console));
            }

            this.Engine = engine;

            this.Audio = audio;
            this.Engine.Components.Add(this.Audio);

            this.File = file;

            this.Event = @event;
            this.Engine.Components.Add(this.Event);

            this.Game = game;

            this.Process = process;
            this.Engine.Components.Add(this.Process);

            this.Screen = screen;
            this.Engine.Components.Add(this.Screen);

            this.Console = console;
            this.Engine.Components.Add(this.Console);

            this.Content = engine.Content;
        }

        public override void Initialize()
        {
        }

        public override void UpdateInput(GameTime time)
        {
            this.Screen.UpdateInput(time);
        }

        public void OnExiting()
        {
            this.Screen.OnExiting();
            this.Game  .OnExiting();
        }

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Audio?.Dispose();
                this.Audio = null;

                this.Console?.Dispose();
                this.Console = null;

                this.Content?.Dispose();
                this.Content = null;

                this.Engine = null;

                this.File?.Dispose();
                this.File = null;

                this.Event?.Dispose();
                this.Event = null;

                this.Game?.Dispose();
                this.Game = null;

                this.Process?.Dispose();
                this.Process = null;

                this.Save?.Dispose();
                this.Save = null;

                this.Screen?.Dispose();
                this.Screen = null;
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}