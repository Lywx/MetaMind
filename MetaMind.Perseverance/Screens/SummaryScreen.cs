using System;
using MetaMind.Engine.Guis.Modules;
using MetaMind.Engine.Screens;
using MetaMind.Perseverance.Guis.Modules;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Screens
{
    public class SummaryScreen : GameScreen
    {
        private readonly IModule summary = new SummaryModule( new SummaryModuleSettings() );

        /// <summary>
        /// Initializes a new instance of the <see cref="SummaryScreen"/> class.
        /// This is the most active screen of all.
        /// </summary>
        public SummaryScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds( 0.5 );
            TransitionOffTime = TimeSpan.FromSeconds( 0.5 );
        }

        public override void Draw( GameTime gameTime )
        {
            ScreenManager.SpriteBatch.Begin();

            MessageManager.Draw( gameTime );

            summary.Draw( gameTime, TransitionAlpha);

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

        public override void Update( GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen )
        {
            if ( IsActive && !coveredByOtherScreen )
            {
                // TODO: not sure it is gonna work
                MessageManager.Update( gameTime );

                Perseverance.Adventure.Update( gameTime );
                
                summary.Update( gameTime );
            }

            base.Update( gameTime, otherScreenHasFocus, coveredByOtherScreen );
        }
    }
}