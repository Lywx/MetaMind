namespace MetaMind.Engine.Components.Content.Texture
{
    using System;
    using Asset;
    using Microsoft.Xna.Framework.Graphics;

    public class ImageAsset : Asset
    {
        public ImageAsset(string name, string asset, ImageDesign design) : base(name)
        {
            if (asset == null)
            {
                throw new ArgumentNullException(nameof(asset));
            }

            this.Design = design;
        }

        #region Resource

        public string Asset { get; set; }

        public ImageDesign Design { get; set; }

        public Texture2D Resource { get; set; }

        #endregion

        #region Conversion

        public Image ToImage()
        {
            return new Image(this.Design, this.Resource);
        }

        #endregion
    }
}
