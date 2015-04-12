// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IView.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System.Collections.Generic;

    using MetaMind.Engine.Guis.Widgets.Items;

    public interface IView : IViewEntity
    {
        dynamic Control { get; set; }

        IViewGraphics Graphics { get; set; }

        List<IViewItem> Items { get; set; }
    }
}