namespace MetaMind.Engine.Component.Content.Asset
{
    using Font;

    public interface IAssetManager
    {
        AssetList<FontAsset> Fonts { get; }
    }
}