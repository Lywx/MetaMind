namespace MetaMind.Engine.Components.Content.Fonts
{
    using Asset;
    using Microsoft.Xna.Framework.Graphics;

    public class MMFontAsset : MMAsset
    {
        public MMFontAsset(
            string name,
            int size,
            string asset,
            SpriteFont resource) : this(name, size, asset)
        {
            this.Resource = resource;
        }

        public MMFontAsset(string name, int size, string asset) : base(name)
        {
            this.Size = size;
            this.Asset = asset;
        }

        public string Asset { get; }

        public int Size { get; }

        public SpriteFont Resource { get; internal set; }
    }
}
