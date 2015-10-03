// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IView.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Controls.Views
{
    using System;
    using System.Collections.Generic;
    using Item;
    using Item.Settings;
    using Layers;
    using Logic;
    using Settings;
    using Visuals;

    public interface IViewOperations
    {
        #region Layer

        T GetLayer<T>() where T : class, IViewLayer;

        #endregion
    }

    /// <summary>
    /// IView define the basic framework for a View object. It allows extending 
    /// component contracts by casting.
    /// </summary>
    public interface IMMViewNode : IViewOperations, IViewComponentOperations, IMMBufferUpdateable
    {
        #region States

        Func<bool> this[ViewState state] { get; set; }

        #endregion
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