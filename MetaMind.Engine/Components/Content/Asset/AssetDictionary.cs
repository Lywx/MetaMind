namespace MetaMind.Engine.Components.Content.Asset
{
    using System.Collections.Generic;
    using Fonts;
    using Texture;

    public class AssetDictionary : Dictionary<string, PackageAsset>
    {
        public FontAsset GetFont(string assetName)
        {
            foreach (var package in this.Values)
            {
                var packageFonts = package.Fonts;
                if (packageFonts.ContainsKey(assetName))
                {
                    return packageFonts[assetName];
                }
            }

            throw new KeyNotFoundException();
        }

        public MMImageAsset GetTexture(string assetName)
        {
            foreach (var package in this.Values)
            {
                var packageTexture = package.Texture;
                if (packageTexture.ContainsKey(assetName))
                {
                    return packageTexture[assetName];
                }
            }

            throw new KeyNotFoundException();
        }
    }
}