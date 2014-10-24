using System;
using MetaMind.Engine.Screens;
using MetaMind.Perseverance.Guis.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MetaMind.Perseverance.Screens
{
    internal class BackgroundScreen : GameScreen
    {
        //---------------------------------------------------------------------

        #region Graphicalc Data

        private Texture2D backgroundTexture;

        private const int particleNumber = 1500;
        private FloatParticleContainer particleContainer = new FloatParticleContainer( particleNumber );

        #endregion Graphicalc Data

        //---------------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public BackgroundScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds( 0.5 );
            TransitionOffTime = TimeSpan.FromSeconds( 0.5 );
        }

        #endregion Constructors

        #region Load and Unload

        /// <summary>
        /// Loads graphics content for this screen. The background texture is quite
        /// big, so we use our own local ContentManager to load it. This allows us
        /// to unload before going from the menus into the game itself, wheras if we
        /// used the shared ContentManager provided by the Game class, the content
        /// would remain loaded forever.
        /// </summary>
        public override void LoadContent()
        {
            backgroundTexture = ContentManager.Load<Texture2D>( @"Textures\Screens\Background\Sea Of Mind" );
        }

        /// <summary>
        /// Unloads graphics content for this screen.
        /// </summary>
        public override void UnloadContent()
        {
            ContentManager.Unload();
        }

        #endregion Load and Unload

        #region Update and Draw

        /// <summary>
        /// Draws the background screen.
        /// </summary>
        public override void Draw( GameTime gameTime )
        {
            var spriteBatch = ScreenManager.SpriteBatch;
            var viewport    = ScreenManager.GraphicsDevice.Viewport;
            var fullscreen  = new Rectangle( 0, 0, viewport.Width, viewport.Height );

            spriteBatch.Begin();

            spriteBatch.Draw( backgroundTexture, fullscreen, new Color( 0, 0, TransitionAlpha / 2 ) );
            particleContainer.Draw( gameTime );

            spriteBatch.End();
        }

        /// <summary>
        /// Updates the background screen. Unlike most screens, this should not
        /// transition off even if it has been covered by another screen: it is
        /// supposed to be covered, after all! This overload forces the
        /// coveredByOtherScreen parameter to false in order to stop the base
        /// Update method wanting to transition off.
        /// </summary>
        public override void Update( GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen )
        {
            particleContainer.Update( gameTime );

            base.Update( gameTime, otherScreenHasFocus, false );
        }

        #endregion Update and Draw
    }
}