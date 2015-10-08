// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Gui
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Provides a basic Software cursor
    /// </summary>
    public class MMCursor
    {
        internal string cursorPath;

        public MMCursor(string path, Vector2 hotspot, int width, int height)
        {
            this.cursorPath = path;
            this.HotSpot    = hotspot;
            this.Width      = width;
            this.Height     = height;
        }

        public int Height { get; set; }

        public int Width { get; set; }

        public Texture2D CursorTexture { get; set; }

        public Vector2 HotSpot { get; set; }

    }
}