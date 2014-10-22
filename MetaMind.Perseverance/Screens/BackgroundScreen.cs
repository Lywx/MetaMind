using System;
using MetaMind.Engine.Screens;
using MetaMind.Perseverance.Guis.Widgets.TimelineHuds;
using MetaMind.Perseverance.Guis.Widgets.TimesphereHuds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MetaMind.Perseverance.Screens
{
    public class BackgroundScreen : GameScreen
    {
        //---------------------------------------------------------------------

        #region Graphicalc Data

        private Texture2D backgroundTexture;

        private TimesphereHud timesphereHud = new TimesphereHud();
        private TimelineHud  timelineHud  = new TimelineHud( new Vector2( 130, 670 - 230 ) );

        #endregion Graphicalc Data

        //---------------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public BackgroundScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds( 2.5 );
            TransitionOffTime = TimeSpan.FromSeconds( 0.5 );
        }

        /// <summary>
        /// Loads graphics content for this screen. The background texture is quite
        /// big, so we use our own local ContentManager to load it. This allows us
        /// to unload before going from the menus into the game itself, wheras if we
        /// used the shared ContentManager provided by the Game class, the content
        /// would remain loaded forever.
        /// </summary>
        public override void LoadContent()
        {
            backgroundTexture = ContentManager.Load<Texture2D>( @"Textures\Screens\Background\Field Of Journey" );
        }

        /// <summary>
        /// Unloads graphics content for this screen.
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        #endregion Constructors

        #region Update and Draw

        /// <summary>
        /// Draws the background screen.
        /// </summary>
        public override void Draw( GameTime gameTime )
        {
            var spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();

            var viewport = ScreenManager.GraphicsDevice.Viewport;
            var fullscreen = new Rectangle( 0, 0, viewport.Width, viewport.Height );
            var color = new Color( 0, 0, TransitionAlpha * 2 / 3 );
            spriteBatch.Draw( backgroundTexture, fullscreen, color );

            timesphereHud.Draw( gameTime, TransitionAlpha );
            timelineHud.Draw( gameTime );

            spriteBatch.End();
        }

        /// <summary>
        /// Updates the background screen. Unlike most screens, this should not
        /// transition off even if it has been covered by another screen: it is
        /// supposed to be covered, after all! This overload forces the
        /// coveredByOtherScreen parameter to false in order to stop the base
        /// Update method wanting to transition off.
        /// </summary>
        public override void Update( GameTime gameTime, bool otherScreenHasFocus,
            bool coveredByOtherScreen )
        {
            timesphereHud.Update( gameTime );
            timelineHud.Update( gameTime );
            base.Update( gameTime, otherScreenHasFocus, false );
        }

        #endregion Update and Draw
    }
}