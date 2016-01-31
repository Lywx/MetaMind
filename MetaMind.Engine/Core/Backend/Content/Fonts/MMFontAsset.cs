namespace MetaMind.Engine.Core.Backend.Content.Fonts
{
    using Asset;
    using Microsoft.Xna.Framework.Graphics;

    public class MMFontAsset : MMAsset
    {
        public MMFontAsset(string name, string path, int size, SpriteFont resource) : this(name, path, size)
        {
            this.Resource = resource;
        }

        public MMFontAsset(string name, string path, int size) : base(name, path)
        {
            this.Size = size;
        }

        public int Size { get; }

        public SpriteFont Resource { get; internal set; }
    }
}
