namespace MetaMind.Engine.Core.Backend.Graphics
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class MMTextureItem
    {
        #region Buffer Data

        public short[] Indexes = new short[6];

        public VertexPositionColorTexture[] Vertices;

        private VertexPositionColorTexture vertexTopLeft;

        private VertexPositionColorTexture vertexTopRight;

        private VertexPositionColorTexture vertexBottomLeft;

        private VertexPositionColorTexture vertexBottomRight;

        #endregion

        #region Geometry Data

        public Vector3 BottomLeft => this.vertexBottomLeft.Position;

        public Vector3 BottomRight => this.vertexBottomRight.Position;

        public Vector3 TopLeft => this.vertexTopLeft.Position;

        public Vector3 TopRight => this.vertexTopRight.Position;

        public Vector3 Center => (this.BottomLeft + this.TopRight) / 2;

        #endregion

        #region Constructors and Finalizer

        public MMTextureItem()
        {
            this.Vertices = new VertexPositionColorTexture[4]
            {
                this.vertexTopLeft,
                this.vertexTopRight,
                this.vertexBottomLeft,
                this.vertexBottomRight,
            };

            this.SetIndexes();
        }

        #endregion

        #region Setters

        public void Set(
            Vector3 position,
            Vector2 size,
            Vector2 origin,
            float rotation,
            MMTextureCoordinate textureCoordinate,
            Color color)
        {
            var width  = size.X;
            var height = size.Y;

            this.SetPosition(
                position,
                width,
                height,
                origin,
                rotation);

            this.SetTextureCoordinate(textureCoordinate);

            this.SetColor(color);
        }

        private void SetColor(Color color)
        {
            this.vertexTopLeft.Color     = color;
            this.vertexTopRight.Color    = color;
            this.vertexBottomLeft.Color  = color;
            this.vertexBottomRight.Color = color;
        }

        private void SetIndexes()
        {
            // TL   TR
            // 0----1 
            // |   /|
            // |  / |
            // | /  |
            // |/   |
            // 2----3
            // BL   BR

            // Set the index buffer for each vertex, using clockwise winding
            this.Indexes[0] = 0;
            this.Indexes[1] = 1;
            this.Indexes[2] = 2;

            this.Indexes[3] = 2;
            this.Indexes[4] = 1;
            this.Indexes[5] = 3;
        }

        private void SetPosition(
            Vector3 position,
            float width,
            float height,
            Vector2 origin,
            float rotation)
        {
            var sin = (float)Math.Sin(rotation);
            var cos = (float)Math.Cos(rotation);

            this.vertexTopLeft.Position.X = position.X - origin.X * cos + origin.Y * sin;
            this.vertexTopLeft.Position.Y = position.Y - origin.X * sin - origin.Y * cos;
            this.vertexTopLeft.Position.Z = position.Z;

            this.vertexTopRight.Position.X = position.X + (-origin.X + width) * cos + origin.Y * sin;
            this.vertexTopRight.Position.Y = position.Y + (-origin.X + width) * sin - origin.Y * cos;
            this.vertexTopRight.Position.Z = position.Z;

            this.vertexBottomLeft.Position.X = position.X - origin.X * cos - (-origin.Y + height) * sin;
            this.vertexBottomLeft.Position.Y = position.Y - origin.X * sin + (-origin.Y + height) * cos;
            this.vertexBottomLeft.Position.Z = position.Z;

            this.vertexBottomRight.Position.X = position.X + (-origin.X + width) * cos - (-origin.Y + height) * sin;
            this.vertexBottomRight.Position.Y = position.Y + (-origin.X + width) * sin + (-origin.Y + height) * cos;
            this.vertexBottomRight.Position.Z = position.Z;
        }

        private void SetTextureCoordinate(MMTextureCoordinate textureCoordinate)
        {
            this.vertexTopLeft.TextureCoordinate = textureCoordinate.TopLeft;

            this.vertexTopRight.TextureCoordinate.X = textureCoordinate.BottomRight.X;
            this.vertexTopRight.TextureCoordinate.Y = textureCoordinate.TopLeft.Y;

            this.vertexBottomLeft.TextureCoordinate.X = textureCoordinate.TopLeft.X;
            this.vertexBottomLeft.TextureCoordinate.Y = textureCoordinate.BottomRight.Y;

            this.vertexBottomRight.TextureCoordinate = textureCoordinate.BottomRight;
        }

        #endregion
    }
}