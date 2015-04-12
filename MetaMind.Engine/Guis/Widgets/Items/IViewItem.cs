// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewItem.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;

    public interface IViewItem : IItemEntity, IDisposable
    {
        dynamic ItemControl { get; set; }

        dynamic ItemData { get; set; }

        IItemGraphics ItemGraphics { get; set; }

        dynamic View { get; }

        dynamic ViewControl { get; }

        dynamic ViewSettings { get; }
    }
}