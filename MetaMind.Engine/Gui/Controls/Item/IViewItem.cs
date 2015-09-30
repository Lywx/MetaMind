// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewItem.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Controls.Item
{
    using System;
    using Layers;
    using Logic;
    using Settings;
    using Views;
    using Visuals;

    public interface IViewItem : IViewItemOperations, IViewItemEntity, IDisposable
    {
        #region Events

        event EventHandler<EventArgs> Selected;

        event EventHandler<EventArgs> Unselected;

        event EventHandler<EventArgs> Swapped;

        event EventHandler<EventArgs> Swapping;

        event EventHandler<EventArgs> Transited;

        #endregion

        #region Item Data

        ItemSettings ItemSettings { get; set; }
        
        /// <summary>
        /// Data that is to be presented.
        /// </summary>
        dynamic ItemData { get; set; }

        IViewItemLogic ItemLogic { get; }

        IViewItemVisual ItemVisual { get; }

        IViewItemLayer ItemLayer { get; }

        #endregion

        #region View Data

        IView View { get; }

        #endregion
    }
}