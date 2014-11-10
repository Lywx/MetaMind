using System;
using MetaMind.Engine;
using MetaMind.Engine.Components;
using MetaMind.Engine.Components.Processes;
using MetaMind.Perseverance.Components;
using MetaMind.Perseverance.Screens;
using MetaMind.Perseverance.Sessions;

namespace MetaMind.Perseverance
{
    public class Perseverance : EngineRunner
    {
        #region Components

        public  static Adventure   Adventure   { get; private set; }
        private static SaveManager SaveManager { get; set; }

        #endregion Components

        public Perseverance( GameEngine engine )
            : base( engine )
        {
            GameEngine.IsMouseVisible  = true;
            GameEngine.IsFixedTimeStep = false;
            
            //-----------------------------------------------------------------
            // save
            SaveManager = SaveManager.GetInstance( Game );
        }

        #region Initialization

        public override void Initialize()
        {
            base.Initialize();
            
            Adventure = Adventure.LoadSave();

            GameEngine.ScreenManager.AddScreen( new BackgroundScreen() );
            GameEngine.ScreenManager.AddScreen( new MotivationScreen() );
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