using C3.Primtive2DXna;
using MetaMind.Engine;
using MetaMind.Engine.Extensions;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Settings;
using Microsoft.Xna.Framework;

namespace MetaMind.Perseverance.Guis.Widgets.TimesphereHuds
{
    public class TimesphereHud : EngineObject
    {
        public void Update( GameTime gameTime )
        {
        }

        public void Draw( GameTime gameTime, byte alpha )
        {
            var spriteBatch = ScreenManager.SpriteBatch;

            Primitives2D.FillRectangle( spriteBatch, new Rectangle( 0, ViewSettings1D.Default.StartPoint.Y - 52, 1600, 2 ), ItemSettings.Default.TransparentColor1.MakeTransparent( alpha ) );
            Primitives2D.FillRectangle( spriteBatch, new Rectangle( 0, ViewSettings1D.Default.StartPoint.Y + 50, 1600, 2 ), ItemSettings.Default.TransparentColor1.MakeTransparent( alpha ) );
            
            Primitives2D.FillRectangle( spriteBatch, new Rectangle( 0, ViewSettings1D.Default.StartPoint.Y - 52, 1600, 103 ), ItemSettings.Default.TransparentColor1.MakeTransparent( alpha ) );
        }
    }
}