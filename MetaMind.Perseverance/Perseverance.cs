using MetaMind.Engine;
using MetaMind.Perseverance.Screens;
using MetaMind.Perseverance.Sessions;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance
{
    public class Perseverance : EngineElement
    {
        public static Adventure Adventure { get; set; }

        #region Constructors

        public Perseverance( Engine.Engine engine )
            : base( engine )
        {
            ConfigureEngine();
        }

        #endregion Constructors

        #region Initialization

        public override void Initialize()
        {
            base.Initialize();
            InitializeScreens();
        }

        private void ConfigureEngine()
        {
            Engine.IsMouseVisible = true;
            Engine.IsFixedTimeStep = false;
        }

        private void InitializeScreens()
        {
            ScreenManager.AddScreen( new BackgroundScreen() );
            ScreenManager.AddScreen( new ChamberScreen() );
        }

        #endregion Initialization

        #region Update and Draw

        public override void Draw( GameTime gameTime )
        {
        }

        public override void Update( GameTime gameTime )
        {
        }

        #endregion Update and Draw

        #region System

        public void Run()
        {
            Engine.Run();
        }

        #endregion System
    }
}