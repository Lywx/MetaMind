namespace MetaMind.Engine.Components.Content.Texture
{
    using System;
    using Asset;
    using Microsoft.Xna.Framework.Graphics;

    public class ImageAsset : Asset
    {
        public ImageAsset(string name, string asset, Designer designer) : base(name)
        {
            if (asset == null)
            {
                throw new ArgumentNullException(nameof(asset));
            }

            if (designer == null)
            {
                throw new ArgumentNullException(nameof(designer));
            }

            this.Designer = designer;
        }

        #region Resource

        public string Asset { get; set; }

        public Designer Designer { get; set; }

        public Texture2D Resource { get; set; }

        #endregion

        #region Conversion

        public Image ToImage()
        {
            return new Image(this.Designer, this.Resource);
        }

        #endregion
    }
}
