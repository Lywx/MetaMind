using MetaMind.Engine;
using MetaMind.Perseverance.Components;
using MetaMind.Perseverance.Screens;
using MetaMind.Perseverance.Sessions;

namespace MetaMind.Perseverance
{
    using System.Linq;
    using System.Windows.Forms;

    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    public class Perseverance : EngineRunner
    {
        public Perseverance(GameEngine engine)
            : base(engine)
        {
            GameEngine.IsMouseVisible = true;asd
            GameEngine.IsFixedTimeStep = false;

            GameEngine.GraphicsManager.PreferredBackBufferWidth = GraphicsSettings.Width;
            GameEngine.GraphicsManager.PreferredBackBufferHeight = GraphicsSettings.Height;
            GameEngine.GraphicsManager.ApplyChanges();

            Screen screen = Screen.AllScreens.First(e => e.Primary);
            GameEngine.Window.Position = new Point(
                screen.Bounds.X + (screen.Bounds.Width - GraphicsSettings.Width) / 2,
                screen.Bounds.Y + (screen.Bounds.Height - GraphicsSettings.Height) / 2);

            //-----------------------------------------------------------------
            // save
            SaveManager = SaveManager.GetInstance(Game);
        }

        #region Components

        public static Adventure Adventure { get; private set; }

        private static SaveManager SaveManager { get; set; }

        #endregion Components

        #region Initialization

        public override void Initialize()
        {
            base.Initialize();

            Adventure = Adventure.LoadSave();

            GameEngine.ScreenManager.AddScreen(new BackgroundScreen());
            GameEngine.ScreenManager.AddScreen(new MotivationScreen());
        }

        #endregion Initialization

        #region System

        public override void OnExiting()
        {
            SaveManager.Save();
        }

        #endregion System
    }
}