namespace MetaMind.Engine.Components.Content.Skins
{
    using Asset;
    using Entities.Controls;

    public class CursorSkinAsset : MMAsset
    {
        public string Asset { get; set; }

        public MMCursor Resource { get; set; }

        #region Constructors

        public CursorSkinAsset(string name)
            : base(name) {}

        public CursorSkinAsset(CursorSkinAsset source)
            : base(source.Name)
        {
            this.Asset = source.Asset;
            this.Resource = source.Resource;
        }

        #endregion
    }
}
