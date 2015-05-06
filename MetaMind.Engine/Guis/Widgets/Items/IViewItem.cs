// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewItem.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items.Layers;
    using MetaMind.Engine.Guis.Widgets.Items.Logic;
    using MetaMind.Engine.Guis.Widgets.Items.Visuals;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    public interface IViewItem : IItemEntity, IDisposable
    {
        #region Events

        event EventHandler<EventArgs> Selected;

        event EventHandler<EventArgs> Unselected;

        event EventHandler<EventArgs> Swapped;

        event EventHandler<EventArgs> Transited;

        #endregion

        #region Item Data

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

        #region Update

        /// <summary>
        /// Update logic related with IView.
        /// </summary>
        /// <param name="time"></param>
        void UpdateView(GameTime time);

        #endregion
    }
}