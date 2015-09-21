namespace MetaMind.Engine.Component.Content.Font
{
    using System;
    using Asset;
    using Microsoft.Xna.Framework.Graphics;

    public class FontAsset : Asset
    {
        public FontAsset(string name, int size, SpriteFont spriteData)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (spriteData == null)
            {
                throw new ArgumentNullException(nameof(spriteData));
            }

            this.Name = name;

            this.SpriteData = spriteData;
            this.MonoData   = new MonoFont(name, size);
        }

        public SpriteFont SpriteData { get; set; }

        public MonoFont MonoData { get; set; }
    }
}