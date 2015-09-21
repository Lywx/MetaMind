namespace MetaMind.Engine.Component.Content.Font
{
    using System;
    using Microsoft.Xna.Framework;

    public class MonoFont
    {
        public MonoFont(string name, int size)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Name = name;
            this.Size = size;
        }

        public string Name { get; }

        public int Size { get; set; }

        public float Aspect { get; set; } = 73f / 102f;

        /// <summary>
        /// This is a margin prefix to monospaced string to left-parallel 
        /// normally spaced string 
        /// </summary>
        public Vector2 Offset { get; set; } = new Vector2(7, 0);

        public Vector2 AsciiSize(float scale)
        {
            return new Vector2(this.Size * this.Aspect * scale, this.Size * scale);
        }
    }
}