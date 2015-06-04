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
    using MetaMind.Engine.Guis.Widgets.Items.Settings;
    using MetaMind.Engine.Guis.Widgets.Views.Layers;
    using MetaMind.Engine.Guis.Widgets.Views.Logic;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;
    using MetaMind.Engine.Guis.Widgets.Views.Visuals;

    public interface IView : IViewEntity
    {
        IViewLogic ViewLogic { get; }

        IViewVisual ViewVisual { get; set; }

        IViewLayer ViewLayer { get; }

        Dictionary<string, object> ViewComponents { get; }

        ViewSettings ViewSettings { get; set; }

        List<IViewItem> ItemsRead { get; }

        List<IViewItem> ItemsWrite { get; set; }

        ItemSettings ItemSettings { get; set; }
    }
}