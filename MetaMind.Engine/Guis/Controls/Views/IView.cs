// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IView.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Controls.Views
{
    using System.Collections.Generic;
    using Items;
    using Items.Settings;
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