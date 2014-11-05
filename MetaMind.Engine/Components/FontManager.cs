using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaMind.Engine.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            return GameEngine.FontManager[ font ].MeasureString( text );
        }
    }

    /// <summary>
    /// Static storage of SpriteFont objects and colors for use throughout the game.
    /// </summary>
    public class FontManager : EngineObject
    {
        private static SpriteFont[] fonts = new SpriteFont[ ( int ) Font.FontNum ];
        private static FontManager singleton;

        public SpriteFont this[ Font font ]
        {
            get { return fonts[ ( int ) font ]; }
            private set { fonts[ ( int ) font ] = value; }
        }

        #region Constructors

        public static FontManager GetInstance()
        {
            return singleton ?? ( singleton = new FontManager() );
        }

        /// <summary>
        /// Load the fonts from the content pipeline.
        /// </summary>
        public void LoadContent()
        {
            // load each font from the content pipeline
            this[ Font.UiRegularFont ] = ContentManager.Load<SpriteFont>( "Fonts/Bitmaps/RegularFont" );
            this[ Font.UiStatisticsFont ] = ContentManager.Load<SpriteFont>( @"Fonts/Bitmaps/StatisticsFont" );
            this[ Font.InfoSimSunFont ] = ContentManager.Load<SpriteFont>( @"Fonts/SpriteFonts/SimSun" );
        }

        /// <summary>
        /// Release all references to the fonts.
        /// </summary>
        public void UnloadContent()
        {
        }

        #endregion Constructors

        #region Text Helper Methods

        public static string ConcatText( string text, int maxLength )
        {
            return text.Length < maxLength ? text : string.Concat( text.Substring( 0, maxLength ), "..." );
        }

        /// <summary>
        /// break text using word by word method.
        /// </summary>
        /// <param name="font"></param>
        /// <param name="text"></param>
        /// <param name="maxlinewidth"></param>
        /// <returns></returns>
        public static string BreakTextIntoLines( SpriteFont font, string text, float maxLineWidth )
        {
            var stringBuilder = new StringBuilder();
            var lines = text.Split( '\n' );
            foreach ( var line in lines )
            {
                var words = line.Split( ' ' );
                var lineWidth = 0f;
                var spaceWidth = font.MeasureString( " " ).X;
                foreach ( var word in words )
                {
                    var size = font.MeasureString( word );
                    if ( lineWidth + size.X < maxLineWidth )
                    {
                        stringBuilder.Append( word + " " );
                        lineWidth += size.X + spaceWidth;
                    }
                    else
                    {
                        stringBuilder.Append( "\n" + word + " " );
                        lineWidth = size.X + spaceWidth;
                    }
                }
                stringBuilder.Append( "\n" );
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Break text using letter by letter method.
        /// </summary>
        /// <param name="font"></param>
        /// <param name="size"></param>
        /// <param name="text"></param>
        /// <param name="maxLineWidth"></param>
        /// <returns></returns>
        private static List<string> BreakTextIntoList( SpriteFont font, float size, string text, float maxLineWidth )
        {
            var textList  = new List<string>();
            var line      = new StringBuilder();
            var lineWidth = 0;
            var index     = 0;
            while ( index < text.Length )
            {
                while ( lineWidth < maxLineWidth )
                {
                    // add current letter
                    line.Append( text[ index ] );
                    // measure length and decide whether to go into next line
                    if ( NextExist( text, index ) )
                    {
                        lineWidth = ( int ) ( font.MeasureString( string.Concat( line.ToString(), text[ index + 1 ] ) ).X * size );
                        index++;
                    }
                    else
                    {
                        lineWidth = ( int ) ( font.MeasureString( line ).X * size );
                        index++;
                        break;
                    }
                }
                textList.Add( line.ToString() );
                // initialize next line
                if ( NextExist( text, index ) )
                {
                    if ( NextContinuation( text, index - 1 ) )
                    {
                        line = new StringBuilder( "-" );
                        lineWidth = 0;
                    }
                    else
                    {
                        line = new StringBuilder();
                        lineWidth = 0;
                    }
                }
                else
                    break;
            }
            return textList;
        }

        private static bool NextExist( string text, int index )
        {
            return index + 1 < text.Count();
        }

        private static bool NextContinuation( string text, int index )
        {
            return text.Substring( index, 1 ).IsAscii() && !char.IsWhiteSpace( text[ index ] ) && !char.IsWhiteSpace( text[ index + 1 ] );
        }

        public List<string> BreakTextIntoList( Font font, float size, string text, float maxLineWidth )
        {
            return BreakTextIntoList( this[ font ], size, text, maxLineWidth );
        }

        #endregion Text Helper Methods

        #region Drawing Helper Methods

        /// <summary>
        /// Draws text centered at particular position.
        /// </summary>
        /// <param name="font">The font used to draw the text.</param>
        /// <param name="text">The text to be drawn</param>
        /// <param name="position">The center position of the text.</param>
        /// <param name="color">The color of the text.</param>
        /// <param name="size">The size of the text.</param>
        public void DrawCenteredText( Font font, string text, Vector2 position, Color color,
            float size )
        {
            // check for trivial text
            if ( String.IsNullOrEmpty( text ) )
                return;

            var spriteFont = FontManager[ font ];
            var avaliableText = GetDisaplayableCharacters( spriteFont, text );
            var textSize = spriteFont.MeasureString( avaliableText );
            var centeredPosition = new Vector2(
                position.X - ( int ) textSize.X * size / 2,
                position.Y - ( int ) textSize.Y * size / 2 );
            ScreenManager.SpriteBatch.DrawString( spriteFont, avaliableText, centeredPosition, color, 0f,
                Vector2.Zero, size, SpriteEffects.None, 1f - position.Y / 720f );
        }

        /// <summary>
        /// Draws the left-top text at particular position.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <param name="text">The text.</param>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        /// <param name="size">The size.</param>
        /// <exception cref="System.ArgumentNullException">Font is not initialized.</exception>
        public void DrawText( Font font, string text, Vector2 position, Color color, float size )
        {
            // check for trivial text
            if ( String.IsNullOrEmpty( text ) )
                return;

            var spriteFont = FontManager[ font ];
            var avaliableText = GetDisaplayableCharacters( spriteFont, text );
            ScreenManager.SpriteBatch.DrawString( spriteFont, avaliableText, position, color, 0f, Vector2.Zero, size, SpriteEffects.None, 0 );
        }

        public string GetDisaplayableCharacters( Font font, string text )
        {
            return GetDisaplayableCharacters( FontManager[ font ], text );
        }

        public string GetDisaplayableCharacters( SpriteFont font, string text )
        {
            return text.Where( t => font.Characters.Contains( t ) ).Aggregate( string.Empty, ( current, t ) => current + t );
        }

        #endregion Drawing Helper Methods
    }
}