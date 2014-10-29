using C3.Primtive2DXna;
using MetaMind.Engine;
using MetaMind.Engine.Extensions;
using MetaMind.Engine.Settings;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Banners
{
    public class TimelineFlash : EngineObject
    {
        private enum LightState
        {
            Static,
            Increment,
            Decrement,
        }

        private FlashSettings flashSettings;

        private LightState state;
        private float      progress;
        private Vector2    position;
        private Vector2    size;
        private Color      color;

        public TimelineFlash( Vector2 position )
        {
            flashSettings = new FlashSettings( position );
        }

        public void Draw( GameTime gameTime )
        {
            Primitives2D.FillRectangle( ScreenManager.SpriteBatch, position.ToRectangle( size ), color );
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
                size     += new Vector2( -10, 0 );
                position += new Vector2( 20, 0 );
                color.R  += 3;
            }
            if ( state == LightState.Decrement )
            {
                progress -= 0.02f;
                size     += new Vector2( 10, 0 );
                position += new Vector2( -20, 0 );
                color.R  -= 3;
            }
            if ( state == LightState.Static )
            {
                progress = 0f;
                size     = flashSettings.Size;
                position = flashSettings.Position;
                color    = flashSettings.Color;
            }
        }
    }

    public class FlashSettings
    {
        public Vector2 Position;
        public Vector2 Size   = new Vector2( 530, 2 );
        public Color   Color  = ColorPalette.TransparentColor1;

        public FlashSettings( Vector2 position )
        {
            Position = position;
        }
    }
}