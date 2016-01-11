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

        public MMRenderer(MMGraphicsDeviceController graphicsDeviceController)
        {
            if (graphicsDeviceController == null)
            {
                throw new ArgumentNullException(nameof(graphicsDeviceController));
            }

            this.GraphicsDeviceController = graphicsDeviceController;
        }

        #endregion

        #region Render Data

        /// <remarks>
        /// Hide the MMObject's getter member.
        /// </remarks>
        private MMGraphicsDeviceController GraphicsDeviceController { get; }

        private SpriteBatch SpriteBatch => this.GraphicsDeviceController.SpriteBatch;

        #endregion

        #region Initialization

        public bool Initialized { get; private set; }

        public void Initialize()
        {
            this.InitializeTextureDrawing();

            this.Initialized = true;
        }

        private void InitializeTextureDrawing()
        {
            // Up direction is consistent with sprite batch and the XNA 3D coordinate 
            this.textureItem = new MMTextureItem();
            this.textureEffect = new BasicEffect(this.GlobalGraphicsDevice)
            {
                TextureEnabled     = true,
                VertexColorEnabled = true,

                World      = Matrix.Identity,
                View       = Matrix.Identity,

                Projection =
                    Matrix.CreateOrthographicOffCenter(0, this.GlobalGraphicsDevice.Viewport.Width,

                        // Notice the Y coordinate goes from bottom of the screen to the
                        // top of the screen. The Y coordinate is inverted. But the Y
                        // axis relative position for the screen is consistent with the
                        // sprite batch.
                        this.GlobalGraphicsDevice.Viewport.Height, 0, 0,

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

        public void DrawMonospacedString(MMFont font, string str, Vector2 position, Color color, float scale)
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
        public void DrawMonospacedString(MMFont font, string str, Vector2 position, Color color, float scale, HoritonalAlignment halignment, VerticalAlignment valignment, int leading = 0)
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

        private void DrawMonospacedChar(MMFont font, char c, Vector2 position, Color color, float scale)
        {
            var str = c.ToString(CultureInfo.InvariantCulture);

            position += font.MonoData.Offset + new Vector2(-font.MeasureString(str, scale).X / 2, 0);

            this.DrawString(font, str, position, color, scale);
        }

        public void DrawString(MMFont font, string str, Vector2 position, Color color, float scale)
        {
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            this.SpriteBatch.DrawString(font.SpriteData, font.AvailableString(str), position, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        public void DrawString(MMFont font, string str, Vector2 position, Color color, float scale, HoritonalAlignment halignment, VerticalAlignment valignment, int leading = 0)
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

        private MMTextureItem textureItem;

        private BasicEffect textureEffect;

        // TODO(Test): Need test.
        public void DrawImmediate(
            Texture2D     texture,
            Vector2?      position = null,
            Rectangle?    destinationRectangle = null,
            Rectangle?    sourceRectangle = null,
            Vector2?      origin = null,
            float         rotation = 0f,
            Vector2?      scale = null,
            Color?        color = null,
            SpriteEffects effects = SpriteEffects.None,
            float         depth = 0f,
            Matrix?       transformation = null)
        {
            // Argument validation
            if (destinationRectangle.HasValue == position.HasValue)
            {
                throw new InvalidOperationException(
                    "Expected destinationRectangle or position, but received neither or both.");
            }

            // Nullable argument default
            if (!origin.HasValue)
            {
                origin = Vector2.Zero;
            }

            if (!scale.HasValue)
            {
                scale = Vector2.One;
            }

            if (!color.HasValue)
            {
                color = Color.White;
            }

            if (!transformation.HasValue)
            {
                transformation = Matrix.Identity;
            }

            // When position is provided
            if (position != null)
            {
                this.DrawImmediate(
                    texture,
                    position.Value,
                    sourceRectangle,
                    color.Value,
                    rotation,
                    origin.Value,
                    scale.Value,
                    effects,
                    depth,
                    transformation.Value);
            }
            else
            {
                this.DrawImmediateInternal(
                    texture,
                    destinationRectangle.Value,
                    sourceRectangle,
                    color.Value,
                    rotation,
                    origin.Value,
                    effects,
                    depth,
                    transformation.Value);
            }
        }

        public void DrawImmediate(
            Texture2D     texture,
            Vector2       position,
            Rectangle?    sourceRectangle,
            Color         color,
            float         rotation,
            Vector2       origin,
            Vector2       scale,
            SpriteEffects effects,
            float         depth,
            Matrix        transformation)

        {
            float width;
            float height;

            if (sourceRectangle.HasValue)
            {
                width = sourceRectangle.Value.Width * scale.X;
                height = sourceRectangle.Value.Height * scale.Y;
            }
            else
            {
                width = texture.Width * scale.X;
                height = texture.Height * scale.Y;
            }

            var size = new Vector2(width, height);

            var destinationRectangle = new Rectangle(
                position.ToPoint(),
                size.ToPoint());

            this.DrawImmediateInternal(
                texture,
                destinationRectangle,
                sourceRectangle,
                color,
                rotation,
                origin * scale,
                effects,
                depth,
                transformation);
        }

        public void DrawImmediate(
            Texture2D texrture,
            Rectangle destinationRectangle,
            Rectangle sourceRectangle,
            Color color)
        {
            this.DrawImmediateInternal(
                texrture,
                destinationRectangle,
                sourceRectangle,
                color,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                0f,
                Matrix.Identity);
        }

        private void DrawImmediateInternal(
            Texture2D     texture,
            Rectangle     destinationRectangle,
            Rectangle?    sourceRectangle,
            Color         color,
            float         rotation,
            Vector2       origin,
            SpriteEffects effect,
            float         depth,
            Matrix        transformation)
        {
            Rectangle sourceRect;

            if (sourceRectangle.HasValue)
            {
                sourceRect = sourceRectangle.Value;
            }
            else
            {
                sourceRect = new Rectangle
                {
                    Width  = texture.Width,
                    Height = texture.Height
                };
            }

            var textureCoordinate = new MMTextureCoordinate(
                topLeft: new Vector2(
                    sourceRect.X / (float)texture.Width,
                    sourceRect.Y / (float)texture.Height), 
                bottomRight: new Vector2(
                    (sourceRect.X + sourceRect.Width) / (float)texture.Width,
                    (sourceRect.Y + sourceRect.Height) / (float)texture.Height));

            if ((effect & SpriteEffects.FlipVertically) != 0)
            {
                textureCoordinate.BottomRight.Y = textureCoordinate.TopLeft.Y;
                textureCoordinate.TopLeft.Y     = textureCoordinate.BottomRight.Y;
            }

            if ((effect & SpriteEffects.FlipHorizontally) != 0)
            {
                textureCoordinate.BottomRight.X = textureCoordinate.TopLeft.X;
                textureCoordinate.TopLeft.X     = textureCoordinate.BottomRight.X;
            }

            this.textureItem.Set(
                new Vector3(destinationRectangle.X, destinationRectangle.Y, depth), 
                new Vector2(destinationRectangle.Width, destinationRectangle.Height),
                origin,
                rotation,
                textureCoordinate,
                color);

            this.textureEffect.World   = transformation;
            this.textureEffect.Texture = texture;

            this.GraphicsDeviceController.EffectPush(this.textureEffect);
            this.GraphicsDeviceController.DrawTexture(this.textureItem);
            this.GraphicsDeviceController.EffectPop();
        }

        #endregion

        #region Draw Helpers

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