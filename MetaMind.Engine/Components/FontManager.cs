// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FontManager.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using MetaMind.Engine.Components.Fonts;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Static storage of SpriteFont objects and colors for use throughout the game.
    /// </summary>
    public class FontManager : EngineObject
    {
        private const float FontMonoSpaceFactor = 2f / 3f;
        // this is a margin prefix to monospaced string to left-parallel normally spaced string
        private const int   FontMonoSpaceMargin = 7;

        private const int   FontMonoSpaceSize   = 18;
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
            this[Font.UiRegularFont] = ContentManager.Load<SpriteFont>(@"Fonts/BitmapFonts/RegularFont");
            this[Font.UiStatisticsFont] = ContentManager.Load<SpriteFont>(@"Fonts/BitmapFonts/StatisticsFont");
            this[Font.UiContentFont] = ContentManager.Load<SpriteFont>(@"Fonts/SpriteFonts/NSimSunFont");
        }

        /// <summary>
        /// Release all references to the fonts.
        /// </summary>
        public void UnloadContent()
        {
        }

        #endregion Load and Unload

        #region Text Drawing

        public static string CropText(string text, int count)
        {
            return text.Length < count ? text : string.Concat(text.Substring(0, count), "...");
        }

        public string CropText(Font font, string text, float size, int maxLength)
        {
            if (maxLength < 1)
            {
                throw new ArgumentOutOfRangeException("maxLength");
            }

            var spriteFont    = FontManager[font];
            var avaliableText = GetDisaplayableCharacters(spriteFont, text);

            var cropped     = false;
            var croppedText = avaliableText;
            var textSize    = spriteFont.MeasureString(croppedText) * size;

            var outsideLength = textSize.X > maxLength;

            while (outsideLength)
            {
                cropped = true;
                croppedText = croppedText.Substring(0, croppedText.Length - 1);
                textSize = spriteFont.MeasureString(croppedText) * size;

                outsideLength = textSize.X > maxLength;
            }

            return croppedText + (cropped ? "..." : string.Empty);
        }

        /// <summary>
        /// Draws text centered at particular position.
        /// </summary>
        /// <param name="font">The font used to draw the text.</param>
        /// <param name="text">The text to be drawn</param>
        /// <param name="position">The center position of the text.</param>
        /// <param name="color">The color of the text.</param>
        /// <param name="scale">The scale of the text.</param>
        public void DrawCenteredText(Font font, string text, Vector2 position, Color color, float scale)
        {
            // check for trivial text
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            var spriteFont    = FontManager[font];
            var avaliableText = GetDisaplayableCharacters(spriteFont, text);

            var textSize         = spriteFont.MeasureString(avaliableText);
            var centeredPosition = new Vector2(
                position.X - (int)textSize.X * scale / 2,
                position.Y - (int)textSize.Y * scale / 2);

            ScreenManager.SpriteBatch.DrawString(spriteFont, avaliableText, centeredPosition, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        public void DrawHCenteredText(Font font, string text, Vector2 position, Color color, float scale)
        {
            position += new Vector2(0, font.MeasureString(text).Y * scale / 2);
            this.DrawCenteredText(font, text, position, color, scale);
        }

        /// <summary>
        /// Draws the left-top monospaced text at particular position.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <param name="text">The text.</param>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        /// <param name="scale">The scale.</param>
        /// <exception cref="System.ArgumentNullException">Font is not initialized.</exception>
        public void DrawMonoSpacedText(Font font, string text, Vector2 position, Color color, float scale)
        {
            // check for trivial text
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            var spriteFont    = FontManager[font];
            var avaliableText = this.GetDisaplayableCharacters(spriteFont, text);

            // HACK: Using existing font is not a good idea
            var asciiFont = FontManager[Font.UiRegularFont];

            var cjkCharacterIndexes         = this.GetNonDisaplayableCharacterIndexes(asciiFont, avaliableText);
            var cjkCharacterAmendedPosition = this.GetCjkCharacterPositionAmendments(cjkCharacterIndexes, avaliableText);
            var cjkCharacterExists = cjkCharacterIndexes.Count > 0;

            for (var i = 0; i < avaliableText.Length; ++i)
            {
                var characterPosition = cjkCharacterExists ? cjkCharacterAmendedPosition[i] : i;
                var amendedPosition   = position + new Vector2(characterPosition * FontMonoSpaceSize * FontMonoSpaceFactor * scale, 0);

                this.DrawMonoSpacedCharacter(font, avaliableText[i], amendedPosition, color, scale);
            }
        }

        /// <summary>
        /// Draws the left-top text at particular position.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <param name="text">The text.</param>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        /// <param name="scale">The scale.</param>
        /// <exception cref="System.ArgumentNullException">Font is not initialized.</exception>
        public void DrawText(Font font, string text, Vector2 position, Color color, float scale)
        {
            // check for trivial text
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            var spriteFont    = FontManager[font];
            var avaliableText = GetDisaplayableCharacters(spriteFont, text);

            ScreenManager.SpriteBatch.DrawString(spriteFont, avaliableText, position, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        public void DrawVCenteredText(Font font, string text, Vector2 position, Color color, float scale)
        {
            position += new Vector2(font.MeasureString(text).X * scale / 2, 0);
            this.DrawCenteredText(font, text, position, color, scale);
        }

        private void DrawMonoSpacedCharacter(Font font, char character, Vector2 position, Color color, float scale)
        {
            var text            = character.ToString(CultureInfo.InvariantCulture);
            var amendedPosition = position + new Vector2(FontMonoSpaceMargin - font.MeasureString(text).X * scale / 2, 0);

            this.DrawText(font, text, amendedPosition, color, scale);
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

        public List<int> GetNonDisaplayableCharacterIndexes(SpriteFont font, string text)
        {
            var indexes = new List<int>();
            for (var i = 0; i < text.Length; i++)
            {
                if (!font.Characters.Contains(text[i]))
                {
                    indexes.Add(i);
                }
            }

            return indexes;
        }

        private List<float> GetCjkCharacterPositionAmendments(List<int> cjkCharacterIndexes, string text)
        {
            var position = 0f;
            var indexes  = new List<float>();

            for (var i = 0; i < text.Length; i++)
            {
                if (cjkCharacterIndexes.Contains(i))
                {
                    position += 0.5f;
                }

                if (i > 0 && cjkCharacterIndexes.Contains(i - 1))
                {
                    position += 0.5f;
                }
                
                indexes.Add(position);
                position += 1f;
            }

            return indexes;

        }

        #endregion
    }
}