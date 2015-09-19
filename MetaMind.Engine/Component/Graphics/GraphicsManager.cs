namespace MetaMind.Engine.Component.Graphics
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class GraphicsManager : GraphicsDeviceManager
    {
        #region Dependency

        protected GraphicsSettings Settings { get; set; }

        protected GameEngine Engine { get; set; }

        #endregion

        public GraphicsManager(GameEngine engine, GraphicsSettings settings)
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            this.Engine   = engine;
            this.Settings = settings;

            this.CreateDevice();

            // Set default resolution
            this.PreferredBackBufferWidth  = 800;
            this.PreferredBackBufferHeight = 600;

            this.ApplyChanges();
        }

        public void Initialize()
        {
            this.ApplySettings();

            this.CentralizeWindow(this.Engine, this.Settings);
        }

        /// <remarks>
        /// Can be only called after GameEngine is constructed.
        /// </remarks>>
        private void CentralizeWindow(GameEngine engine, GraphicsSettings settings)
        {
            var window = engine.Window;
            var screen = settings.Screen;
            var bounds = screen.Bounds;

            window.Position = new Point(
                bounds.X + (bounds.Width - settings.Width) / 2,
                bounds.Y + (bounds.Height - settings.Height) / 2);
        }

        #region Setting Operations

        private void ApplySettings()
        {
            // Fixed drawing order in 3d graphics
            this.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            this.ApplyFrameSettings(this.Engine, this.Settings);
            this.ApplyMouseSettings(this.Engine, this.Settings);
            this.ApplyScreenSettings(this.Engine, this.Settings);
            
            this.ApplyChanges();
        }

        private void ApplyFrameSettings(GameEngine engine, GraphicsSettings settings)
        {
            engine.TargetElapsedTime = TimeSpan.FromMilliseconds(1000 / (double)settings.FPS);
            engine.IsFixedTimeStep = true;
        }

        private void ApplyMouseSettings(GameEngine engine, GraphicsSettings settings)
        {
            engine.IsMouseVisible = settings.IsMouseVisible;
        }

        private void ApplyScreenSettings(GameEngine engine, GraphicsSettings settings)
        {
            // Resolution
            this.PreferredBackBufferWidth = settings.Width;
            this.PreferredBackBufferHeight = settings.Height;

            // Border
            var window = engine.Window;
            window.IsBorderless = settings.IsFullscreen;
        }

        #endregion Graphics Operations
    }
}