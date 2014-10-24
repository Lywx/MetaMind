using System;
using MetaMind.Engine.Components;
using MetaMind.Engine.Screens;
using MetaMind.Perseverance.Guis.Widgets.RibbonHuds;
using MetaMind.Perseverance.Guis.Widgets.TimelineHuds;
using MetaMind.Perseverance.Sessions;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Screens
{
    public class MotivationScreen : GameScreen
    {
        private RibbonHud   ribbonHud   = new RibbonHud();
        private TimelineHud timelineHud = new TimelineHud( new Vector2( 130, 670 - 230 ) );

        /// <summary>
        /// Initializes a new instance of the <see cref="MotivationScreen"/> class.
        /// This is the most active screen of all.
        /// </summary>
        public MotivationScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds( 0.5 );
            TransitionOffTime = TimeSpan.FromSeconds( 0.5 );
        }

        public override void Draw( GameTime gameTime )
        {
            ScreenManager.SpriteBatch.Begin();

            MessageManager.Draw( gameTime );

            ribbonHud  .Draw( gameTime, TransitionAlpha );
            timelineHud.Draw( gameTime );

            ScreenManager.SpriteBatch.End();
        }

        public override void HandleInput()
        {
            InputEventManager   .HandleInput();
            InputSequenceManager.HandleInput();
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
                Perseverance.Adventure.Update( gameTime );

                ribbonHud  .Update( gameTime );
                timelineHud.Update( gameTime );

                MessageManager.Update( gameTime );
            }
            base.Update( gameTime, otherScreenHasFocus, coveredByOtherScreen );
        }
    }
}