namespace MetaMind.Engine.Core.Backend.Content.Texture
{
    using System;
    using Asset;
    using Microsoft.Xna.Framework.Graphics;

    public class MMImageAsset : MMAsset
    {
        public MMImageAsset(string name, string asset, MMImageDesign design) : base(name)
        {
            if (asset == null)
            {
                throw new ArgumentNullException(nameof(asset));
            }

            this.Design = design;
        }

        #region Resource

        public string Asset { get; set; }

        public MMImageDesign Design { get; set; }

        public Texture2D Resource { get; set; }

        #endregion

        #region Conversion

        public MMImage ToImage()
        {
            return new MMImage(this.Design, this.Resource);
        }

        #endregion
    }
}
