namespace MetaMind.Engine.Component.Content.Texture
{
    using System;
    using Asset;
    using Microsoft.Xna.Framework.Graphics;

    public class ImageAsset : Asset
    {
        public ImageAsset(string name, string asset) : base(name)
        {
            if (asset == null)
            {
                throw new ArgumentNullException(nameof(asset));
            }
        }

        public Texture2D Resource { get; set; }

        public string Asset { get; set; }
    }
}
