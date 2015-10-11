// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CursorSkinAsset.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Components.Content.Skins
{
    using Asset;
    using Entities.Controls;

    public class CursorSkinAsset : Asset
    {
        #region Constructors

        public CursorSkinAsset(string name)
            : base(name)
        {
            
        }

        public CursorSkinAsset(CursorSkinAsset source)
            : base(source.Name)
        {
            this.Asset    = source.Asset;
            this.Resource = source.Resource;
        }

        #endregion

        public string Asset { get; set;}

        public MMCursor Resource { get; set; }= null;
    }
}