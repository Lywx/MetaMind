namespace MetaMind.Engine.Components
{
    using System;
    using Audio;
    using Content.Asset;
    using Interop;
    using Interop.Event;
    using Interop.Process;
    using IO;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Services.Console;

    public class MMEngineInterop : MMInputableComponent, IMMEngineInterop
    {
        #region Constructors and Finalizer

        public MMEngineInterop(MMEngine engine, IMMScreenDirector screen, MMConsole console)
            : base(engine)
        {
            if (screen == null)
            {
                throw new ArgumentNullException(nameof(screen));
            }

            if (console == null)
            {
                throw new ArgumentNullException(nameof(console));
            }

            this.Audio = MMAudioManagerFactory.Create(engine);
            this.Engine.Components.Add(this.Audio);

            this.Asset = new MMAssetManager(engine);
            this.Engine.Components.Add(this.Asset);

            this.File = new MMDirectoryManager();

            this.Event = new MMEventManager(engine)
            {
                UpdateOrder = 3
            };
            this.Engine.Components.Add(this.Event);

            this.Game = new MMGameManager(engine);

            this.Process = new MMProcessManager(engine)
            {
                UpdateOrder = 4
            };
            this.Engine.Components.Add(this.Process);

            this.Screen = screen;
            this.Engine.Components.Add(this.Screen);

            this.Console = console;
            this.Engine.Components.Add(this.Console);

            this.Content = engine.Content;
        }

        #endregion

        public IMMAssetManager Asset { get; private set; }

        public IMMAudioManager Audio { get; private set; }

        public MMConsole Console { get; set; }

        public ContentManager Content { get; private set; }

        public IMMDirectoryManager File { get; private set; }

        public IMMEventManager Event { get; private set; }

        public new IMMGameManager Game { get; private set; }

        public IMMProcessManager Process { get; private set; }

        public IMMScreenDirector Screen { get; private set; }

        public IMMSaveManager Save { get; set; }

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

                this.Asset?.Dispose();
                this.Asset = null;

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