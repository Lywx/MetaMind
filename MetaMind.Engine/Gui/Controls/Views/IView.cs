// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IView.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Controls.Views
{
    using System.Collections.Generic;
    using Item;
    using Item.Settings;
    using Layers;
    using Logic;
    using Settings;
    using Visuals;

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