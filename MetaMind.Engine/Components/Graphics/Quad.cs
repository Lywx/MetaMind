namespace MetaMind.Engine.Components.Graphics
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public struct Quad
    {
        #region Buffer Data

        public short[] Indexes;

        public VertexPositionNormalTexture[] Vertices;

        public Vector3 BottomLeft { get; private set; }

        public Vector3 BottomRight { get; private set; }

        public Vector3 TopLeft { get; private set; }

        public Vector3 TopRight { get; private set; }

        public Vector3 Center => (this.BottomLeft + this.TopRight) / 2;

        #endregion

        /// <remarks>
        /// Consistent with sprite batch coordinate.
        /// </remarks>
        public Vector3 Backward;

        /// <remarks>
        /// Consistent with sprite batch coordinate.
        /// </remarks>
        public Vector3 Left;

        public Vector3 Right => -this.Left;

        /// <remarks>
        /// Consistent with sprite batch coordinate.
        /// </remarks>
        public Vector3 Up;

        public Vector3 Down => -this.Up;

        public float Width { get; private set; }

        public float Height { get; private set; }

        public Quad(Vector3 position, Vector3 left, Vector3 up, Vector3 backward)
        {
            // Coordinate axes
            this.Left     = left;
            this.Up       = up;
            this.Backward = backward;

            // Buffers
            this.Vertices = new VertexPositionNormalTexture[4];
            this.Indexes  = new short[6];

            // Size
            this.Width  = 0;
            this.Height = 0;

            // Vertices
            this.BottomLeft  = position;
            this.BottomRight = position;
            this.TopLeft     = position;
            this.TopRight    = position;

            this.SetVertices(position);
            this.SetIndexes();
        }

        public void SetSize(float width, float height)
        {
            this.Width  = width;
            this.Height = height;

            this.SetVertices(this.BottomLeft);
        }

        public void SetVertices(Vector3 position)
        {
            this.BottomLeft  = position;
            this.BottomRight = position     + this.Width  * this.Right;
            this.TopLeft     = position     + this.Height * this.Up;
            this.TopRight    = this.TopLeft + this.Width  * this.Right;

            // Provide a normal for each vertex
            for (var i = 0; i < this.Vertices.Length; i++)
            {
                this.Vertices[i].Normal = this.Backward;
            }

            // Set the position and texture coordinate for each vertex
            this.Vertices[0].Position = this.BottomLeft;
            this.Vertices[1].Position = this.TopLeft;
            this.Vertices[2].Position = this.BottomRight;
            this.Vertices[3].Position = this.TopRight;

            this.SetVerticesTexturePosition();
        }

        private void SetVerticesTexturePosition()
        {
            // Fill in texture coordinates to display full texture on quad
            var textureUpperLeft  = new Vector2(0.0f, 0.0f);
            var textureUpperRight = new Vector2(1.0f, 0.0f);
            var textureLowerLeft  = new Vector2(0.0f, 1.0f);
            var textureLowerRight = new Vector2(1.0f, 1.0f);

            this.Vertices[0].TextureCoordinate = textureLowerLeft;
            this.Vertices[1].TextureCoordinate = textureUpperLeft;
            this.Vertices[2].TextureCoordinate = textureLowerRight;
            this.Vertices[3].TextureCoordinate = textureUpperRight;
        }

        private void SetIndexes()
        {
            // Set the index buffer for each vertex, using clockwise winding
            this.Indexes[0] = 0;
            this.Indexes[1] = 1;
            this.Indexes[2] = 2;
            this.Indexes[3] = 2;
            this.Indexes[4] = 1;
            this.Indexes[5] = 3;
        }
    }
}