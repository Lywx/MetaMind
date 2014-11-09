﻿using System;
using MetaMind.Engine.Guis.Widgets;
using MetaMind.Engine.Screens;
using MetaMind.Perseverance.Guis.Widgets.Motivations;
using MetaMind.Perseverance.Guis.Widgets.Synchronizations;
using MetaMind.Perseverance.Guis.Widgets.Tasks;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Screens
{
    public class MotivationScreen : GameScreen
    {
        private readonly IWidget synchronization    = new SynchronizationHud( Perseverance.Adventure.Cognition.Synchronization, new SynchronizationHudSettings() );
        private readonly IWidget motivation         = new MotivationExchange();

        public MotivationScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds( 0.5 );
            TransitionOffTime = TimeSpan.FromSeconds( 0.5 );
            
            IsPopup = true;
        }

        public override void Draw( GameTime gameTime )
        {
            base.Draw( gameTime );

            ScreenManager.SpriteBatch.Begin();

            MessageManager .Draw( gameTime );
            motivation     .Draw( gameTime, TransitionAlpha );
            synchronization.Draw( gameTime, TransitionAlpha );

            ScreenManager.SpriteBatch.End();
        }

        public override void HandleInput()
        {
            InputEventManager   .HandleInput();
            InputSequenceManager.HandleInput();
            
            motivation          .HandleInput();
        }

        public override void Update( GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen )
        {
            if ( IsActive && !coveredByOtherScreen )
            {
                InputEventManager   .Update( gameTime );
                InputSequenceManager.Update( gameTime );
                MessageManager      .Update( gameTime );

                Perseverance.Adventure.Update( gameTime );

                motivation     .Update( gameTime );
                synchronization.Update( gameTime );
            }
            base.Update( gameTime, otherScreenHasFocus, coveredByOtherScreen );
        }
    }
}