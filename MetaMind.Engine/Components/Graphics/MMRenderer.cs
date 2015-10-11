namespace MetaMind.Engine.Components.Graphics
{
    using System;
    using System.Globalization;
    using Content.Fonts;
    using Entities.Graphics.Fonts;
    using Extensions;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class MMRenderer : IMMRenderer
    {
        #region Constructors

        public MMRenderer(SpriteBatch spriteBatch)
        {
            if (spriteBatch == null)
            {
                throw new ArgumentNullException(nameof(spriteBatch));
            }

            this.SpriteBatch = spriteBatch;
        }

        #endregion

        #region Dependency

        public SpriteBatch SpriteBatch { get; }

        #endregion

        #region Initialization

        public void Initialize()
        {
        }

        #endregion

        #region Sprite Batch

        public void Begin(BlendState blendState = null,
            SamplerState samplerState           = null,
            DepthStencilState depthStencilState = null,
            RasterizerState rasterizerState     = null,
            Effect effect                       = null,
            Matrix? transformMatrix             = null)
        {
            this.SpriteBatch.Begin(SpriteSortMode.Immediate, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
        }

        public void End()
        {
            this.SpriteBatch.End();
        }

        #endregion

        #region Draw String

        public void DrawMonospacedString(Font font, string str, Vector2 position, Color color, float scale)
        {
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            var displayable = font.AvailableString(str);

            var CJKCharIndexes = displayable.CJKUniqueCharIndexes();
            var CJKCharAmendedPosition = displayable.CJKUniqueCharAmendedPosition(CJKCharIndexes);

            var isCJKCharExisting = CJKCharIndexes.Count > 0;

            for (var i = 0; i < displayable.Length; ++i)
            {
                var charPosition = isCJKCharExisting ? CJKCharAmendedPosition[i] : i;
                var amendedPosition = position + new Vector2(charPosition * font.MonoData.AsciiSize(scale).X, 0);

                this.DrawMonospacedChar(font, displayable[i], amendedPosition, color, scale);
            }
        }

        /// <param name="font"></param>
        /// <param name="str"></param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <param name="scale"></param>
        /// <param name="halignment"></param>
        /// <param name="valignment"></param>
        /// <param name="leading">Vertical distance from line to line</param>
        public void DrawMonospacedString(Font font, string str, Vector2 position, Color color, float scale, HoritonalAlignment halignment, VerticalAlignment valignment, int leading = 0)
        {
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            if (leading == 0)
            {
                leading = (int)font.MonoData.AsciiSize(scale).Y * 2;
            }

            var lines = str.Split('\n');

            for (var i = 0; i < lines.Length; ++i)
            {
                var line = lines[i];

                var lineString = font.AvailableString(line);
                var lineSize = font.MeasureMonospacedString(lineString, scale);

                var linePosition = position;

                linePosition += new Vector2(0, i * leading);

                if (valignment == VerticalAlignment.Center)
                {
                    linePosition -= new Vector2(0, lineSize.Y / 2);
                }

                if (valignment == VerticalAlignment.Top)
                {
                    linePosition -= new Vector2(0, lineSize.Y);
                }

                if (halignment == HoritonalAlignment.Center)
                {
                    linePosition -= new Vector2(lineSize.X / 2, 0);
                }

                if (halignment == HoritonalAlignment.Left)
                {
                    linePosition -= new Vector2(lineSize.X, 0);
                }

                this.DrawMonospacedString(font, lineString, linePosition, color, scale);
            }
        }

        private void DrawMonospacedChar(Font font, char c, Vector2 position, Color color, float scale)
        {
            var str = c.ToString(CultureInfo.InvariantCulture);

            position += font.MonoData.Offset + new Vector2(-font.MeasureString(str, scale).X / 2, 0);

            this.DrawString(font, str, position, color, scale);
        }

        public void DrawString(Font font, string str, Vector2 position, Color color, float scale)
        {
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            this.SpriteBatch.DrawString(font.SpriteData, font.AvailableString(str), position, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        public void DrawString(Font font, string str, Vector2 position, Color color, float scale, HoritonalAlignment halignment, VerticalAlignment valignment, int leading = 0)
        {
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            if (leading == 0)
            {
                leading = (int)font.MonoData.AsciiSize(scale).Y * 2;
            }

            var lines = str.Split('\n');

            for (var i = 0; i < lines.Length; ++i)
            {
                var line = lines[i];

                var lineString = font.AvailableString(line);
                var lineSize = font.MeasureString(lineString, scale);

                var linePosition = position;

                linePosition += new Vector2(0, i * leading);

                if (valignment == VerticalAlignment.Center)
                {
                    linePosition -= new Vector2(0, lineSize.Y / 2);
                }

                if (valignment == VerticalAlignment.Top)
                {
                    linePosition -= new Vector2(0, lineSize.Y);
                }

                if (halignment == HoritonalAlignment.Center)
                {
                    linePosition -= new Vector2(lineSize.X / 2, 0);
                }

                if (halignment == HoritonalAlignment.Left)
                {
                    linePosition -= new Vector2(lineSize.X, 0);
                }

                this.DrawString(font, lineString, linePosition, color, scale);
            }
        }

        #endregion

        #region Draw Texture

        public void Draw(Texture2D texture, Rectangle destination, Color color, float depth)
        {
            if (destination.Width > 0 && 
                destination.Height > 0)
            {
                this.SpriteBatch.Draw(texture, destination, null, color, 0.0f, Vector2.Zero, SpriteEffects.None, depth);
            }
        }

        public void Draw(Texture2D texture, Rectangle destination, Rectangle source, Color color, float depth)
        {
            if (source.Width > 0 && 
                source.Height > 0 && 
                destination.Width > 0 && 
                destination.Height > 0)
            {
                this.SpriteBatch.Draw(texture, destination, source, color, 0.0f, Vector2.Zero, SpriteEffects.None, depth);
            }
        }

        public void Draw(Texture2D texture, int x, int y, Color color, float depth)
        {
            this.SpriteBatch.Draw(texture, new Vector2(x, y), null, color, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, depth);
        }

        public void Draw(Texture2D texture, int x, int y, Rectangle source, Color color, float depth)
        {
            if (source.Width > 0 && 
                source.Height > 0)
            {
                this.SpriteBatch.Draw(texture, new Vector2(x, y), source, color, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, depth);
            }
        }

        #endregion
    }
}