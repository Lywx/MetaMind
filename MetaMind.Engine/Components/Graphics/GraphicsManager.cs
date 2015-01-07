namespace MetaMind.Engine.Components.Graphics
{
    using MetaMind.Engine.Settings.Loaders;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class GraphicsManager : GraphicsDeviceManager, ISettingLoader
    {
        #region Singleton

        private static GraphicsManager singleton;

        public static GraphicsManager GetInstance(Game game)
        {
            return singleton ?? (singleton = new GraphicsManager(game));
        }


        #endregion

        private GraphicsManager(Game game)
            : base(game)
        {
        }

        public string ConfigurationFile
        {
            get
            {
                return "Graphics.txt";
            }
        }

        public void CenterWindow()
        {
            // center game window
            var screen = GraphicsSettings.Screen;
            GameEngine.Instance.Window.Position = new Point(
                screen.Bounds.X + (screen.Bounds.Width - GraphicsSettings.Width) / 2,
                screen.Bounds.Y + (screen.Bounds.Height - GraphicsSettings.Height) / 2);

            // set width and height
            this.PreferredBackBufferWidth = GraphicsSettings.Width;
            this.PreferredBackBufferHeight = GraphicsSettings.Height;

            this.ApplyChanges();
        }

        public void Initialize()
        {
            this.LoadSettings();
            
            // fixed drawing order in 3d graphics
            this.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
        }

        public void LoadSettings()
        {
            var dict = SettingLoader.LoadUniquePairs(this);

            var fullscreen = SettingLoader.BooleanValue(dict, "IsFullscreen", false);
            var width  = SettingLoader.MultipleIntValue(dict, "Resolution", 0, 800);
            var height = SettingLoader.MultipleIntValue(dict, "Resolution", 1, 600);
            var fps    = SettingLoader.MultipleIntValue(dict, "Fps", 0, 30);

            var mouseVisible = SettingLoader.BooleanValue(dict, "IsMouseVisible", true);

            GraphicsSettings.SetScreenProperties(width, height, fullscreen, fps);
            GraphicsSettings.SetMouseProperties(mouseVisible);

            this.PreferredBackBufferWidth  = GraphicsSettings.Width;
            this.PreferredBackBufferHeight = GraphicsSettings.Height;

            this.ApplyChanges();
        }
    }
}