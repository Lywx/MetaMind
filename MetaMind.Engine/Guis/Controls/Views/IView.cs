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

    public interface IView : IViewEntity, IViewOperations, IViewComponentOperations 
    {
        #region View Data

        IViewLogic ViewLogic { get; set; }

        IViewVisual ViewVisual { get; set; }

        IViewLayer ViewLayer { get; set; }

        Dictionary<string, object> ViewComponents { get; }

        ViewSettings ViewSettings { get; set; }

        #endregion

        #region Item Data

        List<IViewItem> ItemsRead { get; }

        /// <summary>
        /// Items collection that is used to write to next frame.
        /// </summary>
        /// <remarks>
        /// This collection should avoid being written twice in one frame, 
        /// because of the possible operation collision using the ItemsRead data.
        /// </remarks>
        List<IViewItem> ItemsWrite { get; set; }

        ItemSettings ItemSettings { get; set; }

        #endregion
        
    }
}