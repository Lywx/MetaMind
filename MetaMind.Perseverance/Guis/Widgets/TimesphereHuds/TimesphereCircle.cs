using System;
using MetaMind.Engine;
using MetaMind.Engine.Extensions;
using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MetaMind.Perseverance.Guis.Widgets.TimesphereHuds
{
    public class TimesphereCircle : EngineObject
    {
        private Point size;
        private Point center;
        private Texture2D circleTexture;
        private Texture2D patternTexture;
        private Color color;
        private float rotation;
        private Random random;

        private bool hasPattern;

        public TimesphereCircle( Point center, Point size, Color color, bool hasPattern )
        {
            this.center = center;
            this.size = size;
            this.color = color;
            this.hasPattern = hasPattern;

            circleTexture = ContentManager.Load<Texture2D>( "Textures/UIs/Timesphere Circle" );

            if ( hasPattern )
            {
                patternTexture = ContentManager.Load<Texture2D>( "Textures/UIs/Timesphere Circle Pattern" );
                random = new Random( ( int ) DateTime.Now.Ticks * DateTime.Now.Millisecond );
            }
        }

        public void Update( GameTime gameTime )
        {
            if ( hasPattern )
                rotation += 0.05f * ( float ) random.NextDouble() + 0.02f;
        }

        public void Draw( GameTime gameTime, byte alpha )
        {
            var spriteBatch = ScreenManager.SpriteBatch;

            var circleDestination = new Rectangle( center.X, center.Y, size.X, size.Y );
            var circleOrigin      = new Vector2( circleTexture.Width / 2f, circleTexture.Height / 2f );
            spriteBatch.Draw( circleTexture, circleDestination, null, color.MakeTransparent( alpha ), rotation, circleOrigin, SpriteEffects.None, 0f );

            if ( hasPattern )
            {
                var patternDestination = new Rectangle( center.X, center.Y, ( int ) ( size.X * 0.95f ), ( int ) ( size.Y * 0.95f ) );
                var patternOrigin      = new Vector2  ( patternTexture.Width / 2f, patternTexture.Height / 2f );

                spriteBatch.Draw( patternTexture, patternDestination, null, ItemSettings.Default.TransparentColor3.MakeTransparent( alpha ), rotation, patternOrigin, SpriteEffects.None, 0f );
            }
        }
    }
}