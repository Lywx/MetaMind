namespace MetaMind.Engine.Components.Graphics
{
    using Content.Fonts;
    using Entities.Graphics.Fonts;
    using Extensions;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Globalization;

    public class MMRenderer : MMObject, IMMRenderer
    {
        #region Constructors

        public MMRenderer(MMRendererManager manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            this.Manager = manager;
        }

        #endregion

        #region Render Data

        private MMRendererManager Manager { get; }

        private SpriteBatch SpriteBatch => this.Manager.SpriteBatch;

        #endregion

        #region

        public void Initialize()
        {
            this.InitializeTextureDrawing();
        }

        private void InitializeTextureDrawing()
        {
            // Up direction is consistent with sprite batch and the XNA 3D coordinate 
            this.textureQuad       = new Quad(Vector3.Zero, Vector3.Left, Vector3.Up, Vector3.Backward);
            this.textureQuadEffect = new BasicEffect(this.GraphicsDevice)
            {
                TextureEnabled = true,

                World      = Matrix.Identity,
                View       = Matrix.Identity,

                Projection =
                    Matrix.CreateOrthographicOffCenter(0, this.GraphicsDevice.Viewport.Width,

                        // Notice the Y coordinate goes from bottom of the screen to the
                        // top of the screen. The Y coordinate is inverted. But the Y
                        // axis relative position for the screen is consistent with the
                        // sprite batch.
                        this.GraphicsDevice.Viewport.Height, 0, 0,

                        // Z coordinate goes from 0 to 1.
                        1f)
            };

        }

        #endregion

        #region Batch Operations

        public void Begin()
        {
            this.SpriteBatch.Begin();
        }

        public void Begin(
            BlendState blendState,
            SamplerState samplerState = null,
            DepthStencilState depthStencilState = null,
            RasterizerState rasterizerState = null,
            Effect effect = null,
            Matrix? transformMatrix = null)
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

        private Quad textureQuad;

        private BasicEffect textureQuadEffect;

        public void Draw(Texture2D texture, Rectangle destination, Color color, float depth, Matrix transformation)
        {
            // Set quad size to texture size
            this.textureQuad.SetSize(texture.Width, texture.Height);

            // Set quad effect
            this.textureQuadEffect.World = transformation;

            // The camera move in the opposite direction compared with the
            // texture. However, the Y coordinate is inverted.
            var cameraDestination = new Vector2(-destination.X, destination.Y);

            // Camera Look From the backward to forward. The camera is inverted.
            this.textureQuadEffect.View = Matrix.CreateLookAt(new Vector3(cameraDestination, 0) + Vector3.Backward, new Vector3(cameraDestination, 0), Vector3.Down);
            this.textureQuadEffect.Texture =en

            this.Manager.VertexColorEnabled = true;
            this.Manager.SetTexture(texture);

            this.Manager.PushEffect(this.textureQuadEffect);

            this.Manager.DrawIndexedPrimitives(PrimitiveType.TriangleList, quad.Vertices, 0, 4, quad.Indexes, 0, 2);

            this.Manager.PopEffect();
            this.Manager.SetTexture(null);
        }

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