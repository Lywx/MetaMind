using System;
using MetaMind.Engine.Components;
using MetaMind.Engine.Guis.Widgets;
using MetaMind.Engine.Screens;
using MetaMind.Perseverance.Guis.Widgets.FeelingWidgets;
using MetaMind.Perseverance.Guis.Widgets.RibbonHuds;
using MetaMind.Perseverance.Guis.Widgets.TimelineHuds;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Screens
{
    public class MotivationScreen : GameScreen
    {
        private RibbonHud   ribbonHud   = new RibbonHud();
        private TimelineHud timelineHud = new TimelineHud( new Vector2( 130, 670 - 230 ) );
        private IWidget     feeling     = new FeelingWidget();

        public MotivationScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds( 0.5 );
            TransitionOffTime = TimeSpan.FromSeconds( 0.5 );
        }

        public override void Draw( GameTime gameTime )
        {
            ScreenManager.SpriteBatch.Begin();

            MessageManager.Draw( gameTime );
            
            ribbonHud     .Draw( gameTime, TransitionAlpha );
            timelineHud   .Draw( gameTime );
            feeling       .Draw( gameTime );

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

                ribbonHud  .Update( gameTime );
                timelineHud.Update( gameTime );
                feeling    .Update( gameTime );

            }
            base.Update( gameTime, otherScreenHasFocus, coveredByOtherScreen );
        }
    }
}