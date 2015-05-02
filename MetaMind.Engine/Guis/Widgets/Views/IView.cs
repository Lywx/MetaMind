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
    using MetaMind.Engine.Guis.Widgets.Views.Extensions;
    using MetaMind.Engine.Guis.Widgets.Views.Logic;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;
    using MetaMind.Engine.Guis.Widgets.Views.Visuals;

    public interface IView : IViewEntity
    {
        IViewLogic ViewLogic { get; }

        IViewVisual ViewVisual { get; set; }

        List<IViewItem> ViewItems { get; }

        IViewExtension ViewExtension { get; }

        ViewSettings ViewSettings { get; set; }
    }
}