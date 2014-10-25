﻿using System;
using MetaMind.Engine.Screens;
using MetaMind.Perseverance.Guis.Modules;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Screens
{
    public class ComputingScreen : GameScreen
    {
        private IModule planning;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComputingScreen"/> class.
        /// This is the most active screen of all.
        /// </summary>
        public ComputingScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds( 0.5 );
            TransitionOffTime = TimeSpan.FromSeconds( 0.5 );

            planning = new PlanningModule( PlanningModuleSettings.Default );
        }

        public override void Draw( GameTime gameTime )
        {
            ScreenManager.SpriteBatch.Begin();

            MessageManager.Draw( gameTime );

            planning.Draw( gameTime );

            ScreenManager.SpriteBatch.End();
        }

        public override void HandleInput()
        {
            InputEventManager.HandleInput();
            InputSequenceManager.HandleInput();

            planning.HandleInput();
        }

        public override void LoadContent()
        {
            ScreenManager.Game.ResetElapsedTime();
        }

        public override void Update( GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen )
        {
            if ( IsActive && !coveredByOtherScreen )
            {
                Perseverance.Adventure.Update( gameTime );
                MessageManager.Update( gameTime );

                planning.Update( gameTime );
            }

            base.Update( gameTime, otherScreenHasFocus, coveredByOtherScreen );
        }
    }
}