namespace MetaMind.Engine.Components.Content.Fonts
{
    using System;
    using Microsoft.Xna.Framework.Graphics;

    public class MMFont
    {
        public MMFont(MMFontAsset asset)
        {
            this.Name       = asset.Name;
            this.SpriteData = asset.Resource;
            this.MonoData   = new MMMonoFont(asset.Name, asset.Size, asset.Resource);
        }

        public MMFont(string name, int size, SpriteFont spriteData)
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
            this.MonoData   = new MMMonoFont(name, size, spriteData);
        }

        public string Name { get; }

        public SpriteFont SpriteData { get; private set; }

        public MMMonoFont MonoData { get; private set; }
    }
}