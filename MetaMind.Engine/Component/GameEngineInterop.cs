namespace MetaMind.Engine.Component
{
    using System;
    using Audio;
    using Console;
    using Content.Asset;
    using File;
    using Interop;
    using Interop.Event;
    using Interop.Process;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;

    public class GameEngineInterop : GameControllableComponent, IGameInterop
    {
        #region Constructors and Finalizer

        public GameEngineInterop(GameEngine engine, IGameManager game, IEventManager @event, IProcessManager process, IScreenManager screen, GameConsole console)
            : base(engine)
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
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

            this.Audio = AudioManagerFactory.Create(engine);
            this.Engine.Components.Add(this.Audio);

            this.Asset = new AssetManager(engine);
            this.Engine.Components.Add(this.Asset);

            this.File = new FileManager();

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

        #endregion

        public IAssetManager Asset { get; private set; }

        public IAudioManager Audio { get; private set; }

        public GameConsole Console { get; set; }

        public ContentManager Content { get; private set; }

        public IFileManager File { get; private set; }

        public IEventManager Event { get; private set; }

        public new IGameManager Game { get; private set; }

        public IProcessManager Process { get; private set; }

        public IScreenManager Screen { get; private set; }

        public ISaveManager Save { get; set; }

        #region Initialization

        public override void Initialize()
        {
        }

        #endregion

        #region Update

        public override void UpdateInput(GameTime time)
        {
            this.Screen.UpdateInput(time);
        }

        #endregion

        #region Exit

        public void OnExiting()
        {
            this.Screen.OnExiting();
            this.Game  .OnExiting();
        }

        #endregion

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