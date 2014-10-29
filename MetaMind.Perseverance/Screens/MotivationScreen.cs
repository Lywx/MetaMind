using System;
using MetaMind.Engine.Components;
using MetaMind.Engine.Guis.Widgets;
using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Engine.Screens;
using MetaMind.Engine.Settings;
using MetaMind.Perseverance.Guis.Widgets.Motivations;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Screens
{
    public class MotivationScreen : GameScreen
    {
        private IWidget     feeling     = new MotivationExchange();

        public MotivationScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds( 0.5 );
            TransitionOffTime = TimeSpan.FromSeconds( 0.5 );
        }

        public override void Draw( GameTime gameTime )
        {
            ScreenManager.SpriteBatch.Begin();

            MessageManager.Draw( gameTime );
            feeling       .Draw( gameTime, TransitionAlpha);

            ScreenManager.SpriteBatch.End();
        }

        public override void HandleInput()
        {
            InputEventManager   .HandleInput();
            InputSequenceManager.HandleInput();
            
            feeling             .HandleInput();
        }

        public override void LoadContent()
        {
            ScreenManager.Game.ResetElapsedTime();
        }

        public override void Update( GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen )
        {
            if ( IsActive && !coveredByOtherScreen )
            {
                InputEventManager   .Update( gameTime );
                InputSequenceManager.Update( gameTime );
                MessageManager      .Update( gameTime );

                Perseverance.Adventure.Update( gameTime );

                feeling.Update( gameTime );

            }
            base.Update( gameTime, otherScreenHasFocus, coveredByOtherScreen );
        }
    }
}