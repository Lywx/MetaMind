namespace MetaMind.Engine.Core
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using Backend;
    using Microsoft.Xna.Framework;
    using Services;

    public class MMEngine : Game, IMMEngine
    {
        private IMMEngineGraphics graphics;

        private IMMEngineNumerical numerical;

        private IMMEngineInterop interop;

        private IMMEngineInput input;

        #region Constructors

        public MMEngine()
        {
            this.Content.RootDirectory = "Content";

            // TODO(Minor, Mouse): Remove this to custom cursor
            this.IsMouseVisible = true;
        }

        #endregion

        #region Component Service Provider

        public static IMMEngineService Service { get; private set; }

        #endregion

        #region Components

        public IMMEngineGraphics Graphics
        {
            get
            {
                return this.graphics ?? (this.graphics = new MMEngineNullGraphics());
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (this.graphics != null)
                {
                    throw new InvalidOperationException();
                }

                this.graphics = value;
            }
        }

        public IMMEngineNumerical Numerical
        {
            get
            {
                return this.numerical
                       ?? (this.numerical = new MMEngineNumerical());
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (this.numerical != null)
                {
                    throw new InvalidOperationException();
                }

                this.numerical = value;
            }
        }

        public IMMEngineInterop Interop
        {
            get
            {
                return this.interop
                       ?? (this.interop = new MMEngineNullInterop(this));
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (this.interop != null)
                {
                    throw new InvalidOperationException();
                }

                this.interop = value;
            }
        }

        public IMMEngineInput Input
        {
            get { return this.input ?? (this.input = new MMEngineNullInput(this)); }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (this.input != null)
                {
                    throw new InvalidOperationException();
                }

                this.input = value;
            }
        }

        #endregion

        #region Initialization

        protected override void Initialize()
        {
            // Service is loaded after MMEngine.Initialize. But it has to 
            // be constructed after Components.
            Service = new MMEngineService(
                new MMEngineGraphicsService(this.Graphics),
                new MMEngineInputService(this.Input),
                new MMEngineInteropService(this.Interop),
                new MMEngineNumericalService(this.Numerical));

            // Graphics has to initialized first
            this.Graphics .Initialize();
            this.Input    .Initialize();
            this.Interop  .Initialize();
            this.Numerical.Initialize();

            base.Initialize();
        }

        #endregion

        #region Load and Unload

        protected override void LoadContent()
        {
            // Load engine persistent asset
            this.Interop.Asset.LoadPackage("Engine.Persistent");

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
        }

        #endregion

        #region Draw and Update  

        protected override void Update(GameTime gameTime)
        {
            this.UpdateInput(gameTime);
            base.Update(gameTime);
        }

        private void UpdateInput(GameTime gameTime)
        {
            this.Input  .UpdateInput(gameTime);
            this.Interop.UpdateInput(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Transparent);

            base.Draw(gameTime);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            this.Interop.OnExiting();
            base        .OnExiting(sender, args);

            this.Dispose(true);
        }

        #endregion

        #region Operations

        public void Restart()
        {
            // Save immediately because the Exit is an asynchronous call, 
            // which may not finished before Process.Start() is called
            this.Interop.Save.Save();

            this.Exit();

            using (var p = Process.GetCurrentProcess())
            {
                Process.Start(Assembly.GetEntryAssembly().Location, p.StartInfo.Arguments);
            }
        }

        #endregion

        #region IDisposable

        private bool IsDisposed { get; set; }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (!this.IsDisposed)
                    {
                        // Don't set to null, for there is null checking in property 
                        // injection
                        this.Interop?.Dispose();
                        this.interop = null;

                        this.Input?.Dispose();
                        this.input = null;

                        this.Graphics?.Dispose();
                        this.graphics = null;
                    }

                    this.IsDisposed = true;
                }
            }
            catch
            {
                // Ignored
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #endregion
    }
}