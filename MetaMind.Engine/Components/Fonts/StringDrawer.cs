namespace MetaMind.Engine.Components.Fonts
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class StringDrawer : IStringDrawer
    {
        #region Dependency

        private SpriteBatch SpriteBatch { get; set; }

        #endregion

        #region Constructors

        public StringDrawer(SpriteBatch spriteBatch)
        {
            if (spriteBatch == null)
            {
                throw new ArgumentNullException("spriteBatch");
            }

            this.SpriteBatch = spriteBatch;
        }

        #endregion

        #region IGameComponent

        public void Initialize()
        {
        }

        #endregion

        #region IStringDrawer

        public void DrawMonospacedString(Font font, string str, Vector2 position, Color color, float scale)
        {
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            var displayable = font.PrintableString(str);

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

            SpriteBatch.DrawString(
                font.GetSprite(),
                font.PrintableString(str),
                position,
                color,
                0f,
                Vector2.Zero,
                scale,
                SpriteEffects.None,
                0);
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

            var stringPrintable = font.PrintableString(str);
            var stringSize = font.MeasureString(stringPrintable, scale);

            position -= new Vector2((int)(stringSize.X / 2), (int)(stringSize.Y / 2));

            this.DrawString(font, stringPrintable, position, color, scale);
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

        private List<float> CJKExclusiveCharAmendedPosition(List<int> CJKExclusiveCharIndexes, string str)
        {
            var position = 0f;
            var indexes = new List<float>();

            for (var i = 0; i < str.Length; i++)
            {
                if (CJKExclusiveCharIndexes.Contains(i))
                {
                    position += 0.5f;
                }

                if (i > 0 && CJKExclusiveCharIndexes.Contains(i - 1))
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