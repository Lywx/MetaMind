﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CursorSkinAsset.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Components.Content.Skins
{
    using Asset;
    using Gui;

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

        public Cursor Resource { get; set; }= null;
    }
}