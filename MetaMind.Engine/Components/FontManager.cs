// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FontManager.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components
{
    using System.Linq;

    using MetaMind.Engine.Components.Fonts;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Static storage of SpriteFont objects and colors for use throughout the game.
    /// </summary>
    public class FontManager : EngineObject
    {
        public SpriteFont this[Font font]
        {
            get
            {
                return fonts[(int)font];
            }

            private set
            {
                fonts[(int)font] = value;
            }
        }

        #region Singleton

        private static readonly SpriteFont[] fonts = new SpriteFont[(int)Font.FontNum];

        private static FontManager singleton;

        public static FontManager GetInstance()
        {
            return singleton ?? (singleton = new FontManager());
        }

        #endregion Singleton

        #region Load and Unload

        /// <summary>
        /// Load the fonts from the content pipeline.
        /// </summary>
        public void LoadContent()
        {
            // load each font from the content pipeline
            this[Font.UiRegularFont] = ContentManager.Load<SpriteFont>("Fonts/Bitmaps/RegularFont");
            this[Font.UiStatisticsFont] = ContentManager.Load<SpriteFont>(@"Fonts/Bitmaps/StatisticsFont");
            this[Font.InfoSimSunFont] = ContentManager.Load<SpriteFont>(@"Fonts/SpriteFonts/SimSun");
        }

        /// <summary>
        /// Release all references to the fonts.
        /// </summary>
        public void UnloadContent()
        {
        }

        #endregion Load and Unload

        #region Text Drawing

        /// <summary>
        /// Draws text centered at particular position.
        /// </summary>
        /// <param name="font">The font used to draw the text.</param>
        /// <param name="text">The text to be drawn</param>
        /// <param name="position">The center position of the text.</param>
        /// <param name="color">The color of the text.</param>
        /// <param name="size">The size of the text.</param>
        public void DrawCenteredText(Font font, string text, Vector2 position, Color color, float size)
        {
            // check for trivial text
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            var spriteFont = FontManager[font];
            var avaliableText = GetDisaplayableCharacters(spriteFont, text);
            var textSize = spriteFont.MeasureString(avaliableText);
            var centeredPosition = new Vector2(
                position.X - (int)textSize.X * size / 2, 
                position.Y - (int)textSize.Y * size / 2);

            ScreenManager.SpriteBatch.DrawString(spriteFont, avaliableText, centeredPosition, color, 0f, Vector2.Zero, size, SpriteEffects.None, 1f - position.Y / 720f);
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
        public void DrawText(Font font, string text, Vector2 position, Color color, float size)
        {
            // check for trivial text
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            var spriteFont    = FontManager[font];
            var avaliableText = GetDisaplayableCharacters(spriteFont, text);
            ScreenManager.SpriteBatch.DrawString(spriteFont, avaliableText, position, color, 0f, Vector2.Zero, size, SpriteEffects.None, 0);
        }

        #endregion Drawing Helper Methods

        #region Characters Filtering

        public string GetDisaplayableCharacters(Font font, string text)
        {
            return this.GetDisaplayableCharacters(FontManager[font], text);
        }

        public string GetDisaplayableCharacters(SpriteFont font, string text)
        {
            return text.Where(t => font.Characters.Contains(t)).Aggregate(string.Empty, (current, t) => current + t);
        }

        #endregion

    }
}