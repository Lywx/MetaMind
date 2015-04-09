namespace MetaMind.Engine.Components.Graphics
{
    using System.Windows.Forms;

    using MetaMind.Engine.Settings.Loaders;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class GraphicsManager : GraphicsDeviceManager, IConfigurationParameterLoader<GraphicsSettings>
    {
        #region Singleton

        private static GraphicsManager singleton;

        public static GraphicsManager GetInstance(Game game)
        {
            return singleton ?? (singleton = new GraphicsManager(game));
        }

        #endregion

        #region Constructors

        private GraphicsManager(Game game)
            : base(game)
        {
            this.PreferredBackBufferWidth  = 800;
            this.PreferredBackBufferHeight = 600;
            this.ApplyChanges();
        }

        #endregion

        #region Configuration

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
            this.ParameterLoad(GameEngine.GraphicsSettings);

            this.WindowCentralize();
        }

        /// <remarks>
        /// Can be only called after GameEngine is constructed.
        /// </remarks>>
        private void WindowCentralize()
        {
            GameEngine.Instance.Window.Position = new Point(
                this.Screen.Bounds.X + (this.Screen.Bounds.Width  - GameEngine.GraphicsSettings.Width)  / 2,
                this.Screen.Bounds.Y + (this.Screen.Bounds.Height - GameEngine.GraphicsSettings.Height) / 2);
        }
    }
}