using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Components
{
    public enum Font
    {
        //---------------------------------------------------------------------
        UiRegularFont,
        UiStatisticsFont,
        
        //---------------------------------------------------------------------
        InfoSimSunFont,
        
        //---------------------------------------------------------------------
        FontNum,
    }

    public static class FontExtension
    {
        public static Vector2 MeasureString( this Font font, string text )
        {
            return Engine.FontManager[ font ].MeasureString( text );
        }
    }
}