namespace MetaMind.Engine.Components.Graphics
{
    using System.Windows.Forms;

    using MetaMind.Engine.Settings.Loaders;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class GraphicsManager : GraphicsDeviceManager, IConfigurationParameterLoader<GraphicsSettings>
    {
        #region Singleton

        private static GraphicsManager Singleton { get; set; }

        public static GraphicsManager GetComponent(GameEngine gameEngine)
        {
            return Singleton ?? (Singleton = new GraphicsManager(gameEngine));
        }

        #endregion

        #region Engine Data

        protected Game Game { get; set; }

        private GameEngineGraphics GameGraphics { get; set; }

        #endregion

        #region Constructors

        private GraphicsManager(GameEngine gameEngine)
            : base(gameEngine)
        {
            this.Game = gameEngine;

            this.GameGraphics = new GameEngineGraphics(gameEngine);

            // Set default resolution
            this.PreferredBackBufferWidth  = 800;
            this.PreferredBackBufferHeight = 600;

            this.ApplyChanges();
        }

        #endregion

        #region Configurations

        public void ConfigurationLoad()
        {
            this.ApplyChanges();
        }

        #endregion

        #region Parameters

        public void ParameterLoad(GraphicsSettings parameter)
        {
            this.Screen = parameter.Screen;
        }

        private Screen Screen { get; set; }

        #endregion

        public void Initialize()
        {
            // fixed drawing order in 3d graphics
            this.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            this.ConfigurationLoad();
            this.ParameterLoad(GameGraphics.Settings);

            this.WindowCentralize();
        }

        /// <remarks>
        /// Can be only called after GameEngine is constructed.
        /// </remarks>>
        private void WindowCentralize()
        {
            this.Game.Window.Position = new Point(
                this.Screen.Bounds.X + (this.Screen.Bounds.Width  - GameGraphics.Settings.Width)  / 2,
                this.Screen.Bounds.Y + (this.Screen.Bounds.Height - GameGraphics.Settings.Height) / 2);
        }
    }
}