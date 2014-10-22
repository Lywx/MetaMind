using C3.Primtive2DXna;
using MetaMind.Engine;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.TimelineHuds
{
    public class TimelineLight : EngineObject
    {
        private enum LightState
        {
            Static,
            Increment,
            Decrement,
        }

        private Point staticSize = new Point( 530, 5 );
        private Point staticPosition;
        private Color statiColor = new Color( 20, 20, 20, 0 );

        private LightState state;
        private float progress;
        private Point position;
        private Point size;
        private Color color = new Color( 20, 20, 20, 0 );

        public TimelineLight( Point position )
        {
            staticPosition = position;
        }

        public void Draw( GameTime gameTime )
        {
            Primitives2D.FillRectangle( ScreenManager.SpriteBatch, new Rectangle( position.X, position.Y, size.X, size.Y ), color );
        }

        public void Update( GameTime gameTime )
        {
            if ( state == LightState.Static && InputSequenceManager.Mouse.IsButtonLeftClicked )
                state = LightState.Increment;
            if ( state == LightState.Increment && progress > 1f )
                state = LightState.Decrement;
            if ( state == LightState.Decrement && progress < 0f )
                state = LightState.Static;

            if ( state == LightState.Increment )
            {
                progress += 0.02f;
                size = new Point( size.X - 10, size.Y );
                position = new Point( position.X + 20, position.Y );
                color.R = ( byte ) ( color.R + 3 );
            }
            if ( state == LightState.Decrement )
            {
                size = new Point( size.X + 10, size.Y );
                position = new Point( position.X - 20, position.Y );
                progress -= 0.02f;
                color.R = ( byte ) ( color.R - 3 );
            }
            if ( state == LightState.Static )
            {
                progress = 0f;
                size = staticSize;
                position = staticPosition;
                color = statiColor;
            }
        }
    }
}