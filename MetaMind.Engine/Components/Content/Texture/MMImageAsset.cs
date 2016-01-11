namespace MetaMind.Engine.Components.Content.Texture
{
    using System;
    using Asset;
    using Microsoft.Xna.Framework.Graphics;

    public class MMImageAsset : MMAsset
    {
        public MMImageAsset(string name, string asset, MMImageSettings settings) : base(name)
        {
            if (asset == null)
            {
                throw new ArgumentNullException(nameof(asset));
            }

            this.Settings = settings;
        }

        #region Resource

        public string Asset { get; set; }

        public MMImageSettings Settings { get; set; }

        public Texture2D Resource { get; set; }

        #endregion

        #region Conversion

        public MMImage ToImage()
        {
            return new MMImage(this.Settings, this.Resource);
        }

        #endregion
    }
}
