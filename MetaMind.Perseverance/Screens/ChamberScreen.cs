using System;
using MetaMind.Engine.Components;
using MetaMind.Engine.Screens;
using MetaMind.Perseverance.Extensions;
using MetaMind.Perseverance.Sessions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MetaMind.Perseverance.Screens
{
    public class ChamberScreen : GameScreen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChamberScreen"/> class.
        /// This is the most active screen of all.
        /// </summary>
        public ChamberScreen()
        {
            Exiting += MainScreen_Exiting;

            TransitionOnTime = TimeSpan.FromSeconds( 0.5 );
            TransitionOffTime = TimeSpan.FromSeconds( 0.5 );
        }
        public override void Draw( GameTime gameTime )
        {
            ScreenManager.SpriteBatch.Begin();

            this.GetAdventure().Draw( gameTime );
            MessageManager.Draw( gameTime );

            ScreenManager.SpriteBatch.End();
        }

        public override void HandleInput()
        {
            InputEventManager.HanleInput();
            InputSequenceManager.HandleInput();

            this.GetAdventure() .HandleInput();
        }

        public override void LoadContent()
        {
            var adventure = Adventure.Load( this );
                            adventure.LoadGui();
            
            this.SetAdventure( adventure );

            ScreenManager.Game.ResetElapsedTime();
        }

        public override void Update( GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen )
        {
            if ( IsActive && !coveredByOtherScreen )
            {
                this.GetAdventure() .Update( gameTime );
                MessageManager.Update( gameTime );
            }
            base.Update( gameTime, otherScreenHasFocus, coveredByOtherScreen );
        }

        private void MainScreen_Exiting( object sender, EventArgs e )
        {
            this.GetAdventure().End();
        }
    }
}