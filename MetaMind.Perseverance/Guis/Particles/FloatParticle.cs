using System;
using C3.Primtive2DXna;
using MetaMind.Engine.Extensions;
using MetaMind.Engine.Settings;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Particles
{
    public class FloatParticle : Particle
    {
        private enum Direction
        {
            Left,
            Right,
        }

        private const int BubbleSeconds = 20;
        private const int Height        = 2;
        private const int Width         = 8;
        
        private       Point     pressure = new Point( 10, 10 );
        private       Point     position;
        private       Direction direction;

        private FloatParticle( Direction direction, Vector2 a, Vector2 v, float lastingSeconds, Color color, float size ) :
            base( a, v, 0f, 0f, 0f, lastingSeconds, color, size )
        {
            this.direction = direction;
            var random = new Random( ( int ) DateTime.Now.Ticks );

            // anywhere on the sides of screen
            position = new Point( this.direction == Direction.Left ? ( int ) ( -Width * Size ) : ScreenManager.GraphicsDevice.Viewport.Width, random.Next( GraphicsSettings.Height ) );

            Position = position.ToVector2();
        }

        public bool IsOutsideScreen
        {
            get
            {
                return ( Position.X + Width * Size < 0 )
                    || ( Position.X > GraphicsSettings.Width )
                    || ( Position.Y + Height * Size < 0 )
                    || ( Position.Y > GraphicsSettings.Height );
            }
        }

        public static FloatParticle RandParticle()
        {
            var random    = new Random( ( int ) DateTime.Now.Ticks );
            var direction = ( Direction ) random.Next( 2 );
            var deep      = random.Next( 1, 10 );
            var size      = deep;

            Vector2 acceleration;
            Vector2 velocity;

            switch ( direction )
            {
                case Direction.Left:
                    {
                        acceleration = new Vector2( random.Next( 5, 10 ), 0 );
                        velocity = new Vector2( random.Next( 30, 50 ), 0 );
                    }
                    break;

                case Direction.Right:
                    {
                        acceleration = new Vector2( random.Next( -10, -5 ), 0 );
                        velocity = new Vector2( random.Next( -50, -30 ), 0 );
                    }
                    break;

                default:
                    {
                        acceleration = Vector2.Zero;
                        velocity = Vector2.Zero;
                    }
                    break;
            }

            var remainingSeconds = random.Next( BubbleSeconds, 2 * BubbleSeconds );
            var color = new Color( random.Next( 0, 100 ) / deep, 50 / deep, 50 / deep, 50 / deep );
            var particle = new FloatParticle( direction, acceleration, velocity, remainingSeconds, color, size );

            return particle;
        }

        public override void Draw( GameTime gameTime )
        {
            ScreenManager.SpriteBatch.FillRectangle( Position, new Vector2( Width * Size, Height * Size ), Color, Angle );
        }

        public override void Update( GameTime gameTime )
        {
            var random = new Random( ( int ) DateTime.Now.Ticks );
            // smooth disappearance
            if ( LastingSeconds < BubbleSeconds )
                Size = Math.Min( LastingSeconds, Size );
            // random water movements
            Acceleration = new Vector2( random.Next( -pressure.X, pressure.X ), random.Next( -pressure.Y, pressure.Y ) );
            base.Update( gameTime );
        }
    }
}