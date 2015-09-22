namespace MetaMind.Engine.Component.Content.Asset
{
    using System.Collections.Generic;
    using Fonts;
    using Texture;

    public class PackageAsset : Asset
    {
        public PackageAsset(string name) : base(name)
        {
        }

        public Dictionary<string, FontAsset> Fonts { get; private set; } =
            new Dictionary<string, FontAsset>();

        public Dictionary<string, ImageAsset> Texture { get; private set; } =
            new Dictionary<string, ImageAsset>();

        public void Add(FontAsset font)
        {
            this.Fonts.Add(font.Name, font);
        }

        public void Add(ImageAsset image)
        {
            this.Texture.Add(image.Name, image);
        }
    }
}
