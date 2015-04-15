namespace MetaMind.Engine.Components.Fonts
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class FontDrawer : IFontDrawer
    {
        private IGameGraphics gameGraphics;

        public FontDrawer(GameEngine gameEngine)
        {
            if (gameEngine == null)
            {
                throw new ArgumentNullException("gameEngine");
            }

            this.gameGraphics = new GameEngineGraphics(gameEngine);
        }

        #region IFontDrawer

        public void DrawMonospacedString(Font font, string str, Vector2 position, Color color, float scale)
        {
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            var displayable = font.DisaplayableString(str);

            var CJKCharIndexes = displayable.CJKExclusiveCharIndexes();
            var CJKCharAmendedPosition = this.CJKExclusiveCharAmendedPosition(CJKCharIndexes, displayable);

            var isCJKCharExisting = CJKCharIndexes.Count > 0;

            for (var i = 0; i < displayable.Length; ++i)
            {
                var charPosition = isCJKCharExisting ? CJKCharAmendedPosition[i] : i;
                var amendedPosition = position + new Vector2(charPosition * font.GetMono().AsciiSize(scale), 0);

                this.DrawMonospacedChar(font, displayable[i], amendedPosition, color, scale);
            }
        }

        public void DrawString(Font font, string str, Vector2 position, Color color, float scale)
        {
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            var spriteBatch = this.gameGraphics.Screens.SpriteBatch;
            spriteBatch.DrawString(font.GetSprite(), font.DisaplayableString(str), position, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        public void DrawStringCenteredH(Font font, string str, Vector2 position, Color color, float scale)
        {
            position += new Vector2(0, font.MeasureString(str).Y * scale / 2);

            this.DrawStringCenteredHV(font, str, position, color, scale);
        }

        public void DrawStringCenteredHV(Font font, string str, Vector2 position, Color color, float scale)
        {
            // check for trivial text
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            var stringDisplayable = font.DisaplayableString(str);
            var stringSize = font.MeasureString(stringDisplayable, scale);

            position -= new Vector2((int)(stringSize.X / 2), (int)(stringSize.Y / 2));

            this.DrawString(font, stringDisplayable, position, color, scale);
        }

        public void DrawStringCenteredV(Font font, string str, Vector2 position, Color color, float scale)
        {
            position += new Vector2(font.MeasureString(str).X * scale / 2, 0);

            this.DrawStringCenteredHV(font, str, position, color, scale);
        }

        private void DrawMonospacedChar(Font font, char khar, Vector2 position, Color color, float scale)
        {
            var str = khar.ToString(CultureInfo.InvariantCulture);

            position += new Vector2(font.GetMono().FontMargin - font.MeasureString(str, scale).X / 2, 0);

            this.DrawString(font, str, position, color, scale);
        }

        #endregion

        #region CJK Char Helper

        private List<float> CJKExclusiveCharAmendedPosition(List<int> cjkExclusiveCharIndexes, string str)
        {
            var position = 0f;
            var indexes = new List<float>();

            for (var i = 0; i < str.Length; i++)
            {
                if (cjkExclusiveCharIndexes.Contains(i))
                {
                    position += 0.5f;
                }

                if (i > 0 && cjkExclusiveCharIndexes.Contains(i - 1))
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